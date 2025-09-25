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
            var Result = _departmentRepository.GetAll();
            return View(Result);
        }
        [HttpGet]
          public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
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
              int Count=  _departmentRepository.Add(department);
                if(Count>0)
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Details(int? id, string ViewName="Details")
        {
            if (id is null) return BadRequest("Invalid id"); //404

            var Department = _departmentRepository.GetById(id.Value);
            if (ViewName == "Details" || ViewName=="Delete") {
                if (Department is null) return NotFound(new
                {
                    StatusCode = 404
                                                       ,
                    message = $"Department with id :{id} is not found"
                });

                return View(ViewName, Department);
            }

            var model = new CreateDepartmentDto()
            {
                Code = Department.Code,
                Name = Department.Name,
                CreatedAt = Department.CreatedAt
            };
               return View(ViewName,model);
        }

        [HttpGet]
        public IActionResult Edit(int?id) {
            //if (id is null) return BadRequest("Invalid id");//404
            //var Department=_departmentRepository.GetById(id.Value);
            //if (Department is null) return NotFound(new
            //{
            //    StatusCode = 404,
            //    message = $"Department with id :{id} is not found"
            //});
            //var model = new CreateDepartmentDto()
            //{
            //    Code = Department.Code,
            //    Name = Department.Name,
            //    CreatedAt = Department.CreatedAt
            //};

            return Details(id,"Edit");
        }
        //[HttpPost]
        //public IActionResult Edit([FromRoute]int ?id,Department model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id != model.Id) return BadRequest("Error 404");

        //        int count=_departmentRepository.UpdateDepartment(model);
        //        if (count > 0) {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]//used with any post action
        public IActionResult Edit([FromRoute]int  id,CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id=id,
                    Name=model.Name,
                    Code=model.Code,
                    CreatedAt=model.CreatedAt

                };
                int count = _departmentRepository.Update(department);
                if (count > 0) {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid id");

            //var Department = _departmentRepository.GetById(id.Value);
            //if (Department is null) return NotFound(
            //    new
            //    {
            //        StatusCode = 404,
            //        message = "Not found"
            //    });
            //CreateDepartmentDto model = new CreateDepartmentDto()
            //{
            //    Code = Department.Code,
            //    Name = Department.Name,
            //    CreatedAt = Department.CreatedAt
            //};


            return Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Department model)
        {
            if (ModelState.IsValid)
            {
                if (id is null) return BadRequest("Invalid id");
            
                int Count = _departmentRepository.Delete(model);
                if (Count > 0)
                    return RedirectToAction("Index");

            }
            return View(model);
        }

        //public IActionResult Delete(int ? id)
        //{
        //    if (id is null) return BadRequest("Invalid id");
        //    var Department = _departmentRepository.GetById(id.Value);
        //    if (Department is null) return NotFound();
        //    int Count = _departmentRepository.Delete(Department);
        //    return RedirectToAction("Index");
        //}
    }
}
