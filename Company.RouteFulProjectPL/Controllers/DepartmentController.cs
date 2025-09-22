using Company.RouteFullProject.DAL.Models;
using Company.RouteFullProject.PL.Dtos;
using Company.RouteFulProject.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.RouteFulProject.PL.Controllers
{
    public class DepartmentController : Controller
    {
    private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var Result = _departmentRepository.GetAllDepartment();
            return View(Result);
        }
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreatedAt = model.CreatedAt,
                };
                _departmentRepository.AddDepartment(department);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
