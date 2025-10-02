using AutoMapper;
using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.BLL.Interfaces;
using Company.RouteFulProject.PL.Dtos;
using Company.RouteFulProject.PL.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.RouteFulProject.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(
            //IEmployeeRepository employeeRepository
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork
            ,IMapper mapper
            )
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork=unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string ? SearchName)
        {
            IEnumerable<Employee> Employees;
            if (string.IsNullOrEmpty(SearchName))
            {
                Employees = await _unitOfWork.EmployeeRepository.GetAllAsync() ;
            }
            else
            {
                Employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchName);
            }
            //Dictionary is inherited from controller class
            //Dictionary:Generic hashtable collection:3Property
            //1.ViewData:Transfer extra information from controller(Action) to view
            //ViewData["Message"] = "Hello from view data";
            //ViewBag.Message = new {Message="Hello from view bag"};



            //2.ViewBag:Transfer extra information from controller(Action) to view

            //3.TempData:Transfer extra information from request to another request
            return View(Employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //var Departments=_departmentRepository.GetAll();
            //ViewData[ "departments"] = Departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                //var Employee = new Employee()
                //{
                //    Name = employeeDto.Name,
                //    Age = employeeDto.Age,
                //    Salary = employeeDto.Salary,
                //    Phone = employeeDto.Phone,
                //    Address = employeeDto.Address,
                //    Email = employeeDto.Email,
                //    CreatedAt = (DateTime)employeeDto.CreatedAt,
                //    HiringDate = employeeDto.HiringDate,
                //    IsActivated = employeeDto.IsActivated,
                //    IsDeleted = employeeDto.IsDeleted,
                //    //Department=employeeDto.Department,
                //    DepartmentId=employeeDto.DepartmentId
                //};

                if(employeeDto.Image is not null)
                {
                   employeeDto.ImageName= DocumentSettings.Upload(employeeDto.Image, "images");
                }

                var Employee = _mapper.Map<Employee>(employeeDto);
                await _unitOfWork.EmployeeRepository.AddAsync(Employee);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0) {

                    TempData["Message"] = $"Employee {employeeDto.Name} is created successfully!";
                
                    return RedirectToAction(nameof(Index));
                        
                        
                        };
                
            }

            //var Departments = _departmentRepository.GetAll();
            //ViewData["departments"] = Departments;
            return View(employeeDto);
        }

        [HttpGet]
        public  async Task<IActionResult> Details([FromRoute] int? id,string ViewName="Details")
        {
            if (id is null) return BadRequest("Invalid id");
            var Employee =await  _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
                var model = _mapper.Map<EmployeeDto>(Employee);
            if (ViewName == "Details" )

            {
                ViewBag.Id = id;

                if(Employee is null) 
                    return BadRequest(new { 
                        StatusCode=404,
                        message=$"Employee with id:{id} is not found"
                    }
                    );
            }
            //var model=_mapper.Map<EmployeeDto>(Employee);
            //var model = new EmployeeDto()
            //{
            //    Name = Employee.Name,
            //    Age = Employee.Age,
            //    Salary = Employee.Salary,
            //    Phone = Employee.Phone,
            //    Address = Employee.Address,
            //    Email = Employee.Email,
            //    CreatedAt = Employee.CreatedAt,
            //    HiringDate = Employee.HiringDate,
            //    IsActivated = Employee.IsActivated,
            //    IsDeleted = Employee.IsDeleted
            //};
            return View(ViewName, model);

        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            //var Departments = _departmentRepository.GetAll();
            //ViewData["departments"] = Departments;
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int ? id,EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //var Employee = new Employee()
                //{
                //    Id = id.Value,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Salary = model.Salary,
                //    Phone = model.Phone,
                //    Address = model.Address,
                //    Email = model.Email,
                //    CreatedAt = (DateTime)model.CreatedAt,
                //    HiringDate = model.HiringDate,
                //    IsActivated = model.IsActivated,
                //    IsDeleted = model.IsDeleted,
                //    //Department=model.Department,
                //    DepartmentId = model.DepartmentId
                //};
                if(model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.Delete(model.ImageName, "images");
                }
                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.Upload(model.Image, "images");
                }
                var Employee= _mapper.Map<Employee>(model);
                Employee.Id = id.Value;

                _unitOfWork.EmployeeRepository.Update(Employee);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0) return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id,"Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int ? id,EmployeeDto model)
        {
            if (id is null) return BadRequest("Invalid id");
            if (ModelState.IsValid)
            {

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id.Value;
                _unitOfWork.EmployeeRepository.Delete(employee);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    if(model.ImageName is not null)
                    {

                      DocumentSettings.Delete(model.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            //var employee = _employeeRepository.GetById(id.Value);

          //  if (employee is null)
          //      return NotFound();

          //int count=  _employeeRepository.Delete(employee);
          //  if(count>0)
          //  return RedirectToAction(nameof(Index));
            return View(model);
        }
    }
}
