using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.BLL.Interfaces;
using Company.RouteFulProject.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.RouteFulProject.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var Employees= _employeeRepository.GetAll();
            return View(Employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                var Employee = new Employee()
                {
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Salary = employeeDto.Salary,
                    Phone = employeeDto.Phone,
                    Address = employeeDto.Address,
                    Email = employeeDto.Email,
                    CreatedAt = employeeDto.CreatedAt,
                    HiringDate = employeeDto.HiringDate,
                    IsActivated = employeeDto.IsActivated,
                    IsDeleted = employeeDto.IsDeleted
                };
                int Count = _employeeRepository.Add(Employee);
                if (Count > 0) return RedirectToAction(nameof(Index));
                
            }
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
            var model = new EmployeeDto()
            {
                Name = Employee.Name,
                Age = Employee.Age,
                Salary = Employee.Salary,
                Phone = Employee.Phone,
                Address = Employee.Address,
                Email = Employee.Email,
                CreatedAt = Employee.CreatedAt,
                HiringDate = Employee.HiringDate,
                IsActivated = Employee.IsActivated,
                IsDeleted = Employee.IsDeleted
            };
            return View(ViewName, model);

        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
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
                    CreatedAt = model.CreatedAt,
                    HiringDate = model.HiringDate,
                    IsActivated = model.IsActivated,
                    IsDeleted = model.IsDeleted
                };

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
            if (id is null) return BadRequest("Invalid id");
            if (ModelState.IsValid)
            {

               
                int Count = _employeeRepository.Delete(model);
                if (Count > 0) return RedirectToAction(nameof(Index));
               
            }
            return View(model);
        }
    }
}
