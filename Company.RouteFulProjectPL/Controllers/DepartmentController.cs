using AutoMapper;
using Company.RouteFullProject.DAL.Models;
using Company.RouteFullProject.PL.Dtos;
using Company.RouteFulProject.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.RouteFulProject.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;

        private readonly IMapper _mapper;

        public DepartmentController(
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? SearchName)
        {
            IEnumerable<Department> departments;
            if (string.IsNullOrEmpty(SearchName))
            {

             departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            }
            else
            {
                departments= await _unitOfWork.DepartmentRepository.GetDepartmentsByNameAsync(SearchName);
            }
            return View(departments);
        }
        [HttpGet]
          public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreatedAt = model.CreatedAt,
                //};
                var department=_mapper.Map<Department>(model);
                      await  _unitOfWork.DepartmentRepository.AddAsync(department);
                int Count =  await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    TempData["Message"] = $"{model.Name} department is created successfully!!";
                    return RedirectToAction("Index");

                }
            }
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName="Details")
        {
            if (id is null) return BadRequest("Invalid id"); //404

            var Department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (ViewName == "Details" || ViewName=="Delete") {
                if (Department is null) return NotFound(new
                {
                    StatusCode = 404
                                                       ,
                    message = $"Department with id :{id} is not found"
                });

                return View(ViewName, Department);
            }

            //var model = new CreateDepartmentDto()
            //{
            //    Code = Department.Code,
            //    Name = Department.Name,
            //    CreatedAt = Department.CreatedAt
            //};
            var model = _mapper.Map<CreateDepartmentDto>(Department);
               return View(ViewName,model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int?id) {
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

            return await Details(id,"Edit");
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
        public async Task<IActionResult> Edit([FromRoute]int?  id,CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Id=id,
                //    Name=model.Name,
                //    Code=model.Code,
                //    CreatedAt=model.CreatedAt

                //};
                if (id is null) return BadRequest("Invalid id");
                var department = _mapper.Map<Department>(model);
                department.Id = id.Value;
                _unitOfWork.DepartmentRepository.Update(department);
                int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0) {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
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


            return await Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id,Department department)
        {
            
                if (id is null) return BadRequest("Invalid id");
                var model = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
                 if(model is null) return NotFound();
                _unitOfWork.DepartmentRepository.Delete(model);
            int Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                    return RedirectToAction("Index");

            
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
