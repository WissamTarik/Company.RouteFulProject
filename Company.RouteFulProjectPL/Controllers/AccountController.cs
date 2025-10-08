using Company.RouteFullProject.DAL.Models;
using Company.RouteFulProject.PL.Dtos;
using Company.RouteFulProject.PL.Helpers;
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

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
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

                if (user is not null)
                {
                    var Flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (Flag)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Invalid SignIn");
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account",
                             new { Email = model.Email, token }, Request.Scheme);
                    var Email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset password",
                        Body = url
                    };
                    var Flag = EmailSettings.SendEmail(Email);
                    if (Flag)
                    {
                        //check your inbox
                        return RedirectToAction("CheckYourInput");
                    }
                }

            }
            ModelState.AddModelError("", "Invalid reset password operation");
            return View("ForgetPassword", model);
        }

        [HttpGet]
        public IActionResult CheckYourInput()
        {
            return View();
        }

        //Pa$$w0rd
        [HttpGet]
        public IActionResult ResetPassword(string Email,  string token)
        {
            TempData["email"] = Email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var email = TempData["email"] as string;
            var token = TempData["token"] as string;

            if (ModelState.IsValid)
            {
                if (email is null || token is null) return BadRequest("Invalid operation");
                var user = await _userManager.FindByEmailAsync(email);
                if(user is not null)
                {
                    var Result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (Result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                }

            }
            ModelState.AddModelError("", "Invalid reset password operation");
            return View();
        }
    
    }
}
