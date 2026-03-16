using AutoMapper;
using DataAccess.SecurityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Areas.Admin.Models;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> a, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["returnUrl"] = returnUrl;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }


            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user is not null)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);

                if (loginResult.Succeeded)
                {
                    TempData["loggedInUserName"] = user.UserName;

                    if (TempData["returnUrl"] is null)
                    {
                        TempData["returnUrl"] = "/Admin/Home/Index";
                    }

                    return Redirect(TempData["returnUrl"]!.ToString()!);
                }
            }


            ModelState.AddModelError("", "Wrong Username / Password.");
            return View(loginViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
