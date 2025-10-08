using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.RouteFulProject.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }
        public IActionResult Index(string SearchName)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchName))
            {
             users=   _userManager.Users.Select( u => new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                });
            }
            else
            {
                users=_userManager.Users.Where(u=>u.FirstName.ToLower().Contains(SearchName.ToLower())).Select(u=>new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName=u.UserName,
                    FirstName=u.FirstName,
                    LastName=u.LastName,
                    Email=u.Email,
                    Roles=_userManager.GetRolesAsync(u).Result
                });
            }
            return View(users);
        }


      
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] string? id,string ViewName="Details")
        {
            if (id == null) return BadRequest("Invalid id");

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return BadRequest(new
                {
                    StatusCode = 404,
                    message = $"User with id:{id} is not found"
                });
            }


            ViewBag.Id = user.Id;
            var model = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };

            return View(ViewName,model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] string ? id)
        {
            return  await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id,UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if(id!=model.Id) return BadRequest(model.Id);
                var user = await _userManager.FindByIdAsync(id);
                if(user is not null)
                {

                    user.UserName = model.UserName;
                    user.FirstName=model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;

                    var Result = await _userManager.UpdateAsync(user);
                    if (Result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] string? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id,UserToReturnDto model)
        {
           if(id !=model.Id) return BadRequest(model.Id);
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByIdAsync(id);
                if(user is not null)
                {
                    var Result = await _userManager.DeleteAsync(user);
                    if (Result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return NotFound();

                }
            }

            return View(model);
        }
    }
}
