using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Company.RouteFulProject.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public IActionResult Index(string? SearchName)
        {
            IEnumerable<RoleToReturnDto> Roles;
            if (string.IsNullOrEmpty(SearchName)) {

                Roles = _roleManager.Roles.Select(r => new RoleToReturnDto
                {

                    Id = r.Id,
                    Name = r.Name,

                });

            }
            else
            {
                Roles = _roleManager.Roles.Where(r => r.Name.ToLower()
                                   .Contains(SearchName.ToLower()))
                                    .Select(r => new RoleToReturnDto()
                                    {
                                        Id = r.Id,
                                        Name = r.Name
                                    });
            }


            return View(Roles);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                var role =await _roleManager.FindByNameAsync(model.Name);
                if(role is not null)
                {
                    ModelState.AddModelError("", $"{model.Name} is already exist");
                }
                else
                {
                    var newRole = new IdentityRole()
                    {
                        Name = model.Name,
                    };
                 var Result= await   _roleManager.CreateAsync(newRole);
                    if (Result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create role");
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] string? id, string ViewName = "Details")
        {

            if (id is null) return BadRequest("Invalid id");
            var role = await _roleManager.FindByIdAsync(id);
            if (role is not null)
            {
                var Role = new RoleToReturnDto()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                ViewBag.Id = role.Id;
                return View(ViewName, Role);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] string? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleToReturnDto model)
        {
            if (model.Id != id) return BadRequest("Invalid id");
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role is not null)
                {
                    var RoleResult = await _roleManager.FindByNameAsync(model.Name);
                    if (RoleResult is null)
                    {
                        role.Name = model.Name;
                        var Result = await _roleManager.UpdateAsync(role);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    ModelState.AddModelError("", $"{role.Name} is already exist");

                }

                else return NotFound();

            }
            return View(model);
        }


    
         [HttpGet]

        public async Task<IActionResult> Delete([FromRoute] string? id)
        {
            return await Details(id, "Delete");
        }
    
         [HttpPost]

        public async Task<IActionResult> Delete([FromRoute] string? id,RoleToReturnDto model)
        {
            if (id != model.Id) return BadRequest("Invalid id");
            if (ModelState.IsValid) {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if(role is not null)
                {
                    var Result=await _roleManager.DeleteAsync(role);
                    if (Result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid operation !!");
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
    public async Task<IActionResult> AddOrRemoveUsers( string? id)
{
            var role = await _roleManager.FindByIdAsync(id);
            if(role is  null)
            {
                return NotFound();
            }
            ViewData["roleId"] = id;

            var userRoles = new List<UserInRoleDto>();
           var  Users = await _userManager.Users.ToListAsync();

            foreach (var user in Users)
            {
                var UserInRole = new UserInRoleDto()
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user,role.Name)) {
                    UserInRole.IsSelected = true;
                }
                else
                {
                    UserInRole.IsSelected = false;
                }
                userRoles.Add(UserInRole);
            }
            return View(userRoles);
}


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers( string ?roleId,List<UserInRoleDto> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.Id);
                    if(appUser is not null)
                    {
                        if (user.IsSelected && ! await _userManager.IsInRoleAsync(appUser,role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appUser,role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                   
                }

                return RedirectToAction("Edit", new { id = roleId });
            }
            return View(users);
        }
    }
}