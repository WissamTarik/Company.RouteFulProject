using AutoMapper;
using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.BLL.Interfaces;
using Company.RouteFulProject.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.RouteFulProject.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository
            //IDepartmentRepository departmentRepository,
            ,IMapper mapper
            )
        {
            _employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public IActionResult Index(string ?SearchInput)
        {
            IEnumerable<Employee> Employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                Employees = _employeeRepository.GetAll() ;
            }
            else
            {
                Employees = _employeeRepository.GetByName(SearchInput);
            }
            //Dictionary is inherited from controller class
            //Dictionary:Generic hashtable collection:3Property
            //1.ViewData:Transfer extra information from controller(Action) to view
            ViewData["Message"] = "Hello from view data";
            ViewBag.Message = new {Message="Hello from view bag"};



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
        public IActionResult Create(EmployeeDto employeeDto)
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

                var Employee = _mapper.Map<Employee>(employeeDto);
                int Count = _employeeRepository.Add(Employee);
                if (Count > 0) {

                    TempData["Message"] = "New Employee is created";

                    return RedirectToAction(nameof(Index));
                        
                        
                        };
                
            }

            //var Departments = _departmentRepository.GetAll();
            //ViewData["departments"] = Departments;
            return View(employeeDto);
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int? id,string ViewName="Details")
        {
            if (id is null) return BadRequest("Invalid id");
            var Employee = _employeeRepository.GetById(id.Value);
            if (ViewName == "Details" || ViewName=="Delete")

            {
                if(Employee is null) 
                    return BadRequest(new { 
                        StatusCode=404,
                        message=$"Employee with id:{id} is not found"
                    }
                    );
                return View(ViewName,Employee);
            }
            var model=_mapper.Map<EmployeeDto>(Employee);
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
        public IActionResult Edit([FromRoute] int? id)
        {
            //var Departments = _departmentRepository.GetAll();
            //ViewData["departments"] = Departments;
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int ? id,EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var Employee = new Employee()
                {
                    Id = id.Value,
                    Name = model.Name,
                    Age = model.Age,
                    Salary = model.Salary,
                    Phone = model.Phone,
                    Address = model.Address,
                    Email = model.Email,
                    CreatedAt = (DateTime)model.CreatedAt,
                    HiringDate = model.HiringDate,
                    IsActivated = model.IsActivated,
                    IsDeleted = model.IsDeleted,
                    //Department=model.Department,
                    DepartmentId = model.DepartmentId
                };
                //var Employee= _mapper.Map<Employee>(model);

                int Count = _employeeRepository.Update(Employee);
                if (Count > 0) return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id,"Delete");
        }
        [HttpPost]
        public IActionResult Delete([FromRoute]int ? id,Employee model)
        {
            //if (id is null) return BadRequest("Invalid id");
            //ModelState.Remove("DepartmentId");
            //if (ModelState.IsValid)
            //{


            //    int Count = _employeeRepository.Delete(model);
            //    if (Count > 0) return RedirectToAction(nameof(Index));

            //}
            var employee = _employeeRepository.GetById(id.Value);

            if (employee is null)
                return NotFound();

          int count=  _employeeRepository.Delete(employee);
            if(count>0)
            return RedirectToAction(nameof(Index));
            return View(model);
        }
    }
}
