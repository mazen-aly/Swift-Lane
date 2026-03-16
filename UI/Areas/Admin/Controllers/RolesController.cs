using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.SecurityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }


        public async Task<IActionResult> Index()
        {
            var rolesList = await _roleManager.Roles.ToListAsync();
            return View(rolesList);
        }


        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNew(string roleName)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var newRole = new ApplicationRole { Name = roleName };

            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
    }
}
