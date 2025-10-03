using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.PL.Dtos;
using Company.RouteFulProjectPL.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.RouteFulProject.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        //P@ssW0rd
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {
                        var User = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,
                        };
                        var Result = await _userManager.CreateAsync(User, model.Password);
                        if (Result.Succeeded)
                        {

                            return RedirectToAction("SignIn");
                        }
                        foreach (var item in Result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                
                 
                
                }
                ModelState.AddModelError("", "Invalid SignUp");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var Flag=await _userManager.CheckPasswordAsync(user, model.Password);
                    if (Flag)
                    {
                        
                       var Result = await _signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe, false);
                        if (Result.Succeeded)
                        {

                        return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ModelState.AddModelError("","Invalid Login !!");
            }

            return View(model);
        }
    }
}
