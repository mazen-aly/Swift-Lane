using AspNetCoreGeneratedDocument;
using BusinessLogic.DTOs;
using DataAccess.SecurityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UI.Areas.Admin.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Viewer")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var usersList = new List<UserViewModel>();

            foreach(var user in await _userManager.Users.ToListAsync())
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersList.Add(new UserViewModel { Id = user.Id, RoleName = roles.First(), UserName = user.UserName});
            }
                

            return View(usersList);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Name");
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel userFromRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(userFromRequest);
            }

            var newUser = new ApplicationUser { UserName = userFromRequest.UserName };

            var result = await _userManager.CreateAsync(newUser, userFromRequest.Password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userFromRequest.UserName);
                var res = await _userManager.AddToRoleAsync(user!, userFromRequest.RoleName);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(userFromRequest);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userFromRequest);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id.ToString()));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
