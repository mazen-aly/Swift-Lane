using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Viewer")]
    public class ShippingTypesController : Controller
    {
        private readonly IShippingTypeService _shippingTypeService;

        public ShippingTypesController(IShippingTypeService shippingTypeService)
        {
            _shippingTypeService = shippingTypeService;
        }


        public async Task<IActionResult> Index()
        {
            var allShippingTypes = await _shippingTypeService.GetAllAsync();
            return View(allShippingTypes);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShippingTypeDto dtoFromRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(dtoFromRequest);
            }

            try
            {
                TempData["Created Successfully"] = await _shippingTypeService.AddNewAsync(dtoFromRequest);
            }
            catch
            {
                TempData["Created Successfully"] = false;
            }


            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var shippingTypeDto = await _shippingTypeService.GetByIdAsync(id);

            if (shippingTypeDto is null)
            {
                TempData["Error"] = true;

                return RedirectToAction(nameof(Index));
            }

            return View(shippingTypeDto);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ShippingTypeDto dtoFromRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(dtoFromRequest);
            }

            try
            {
                TempData["Updated Successfully"] = await _shippingTypeService.UpdateAsync(dtoFromRequest);
            }
            catch
            {
                TempData["Updated Successfully"] = false;
            }

            return RedirectToAction(nameof(Update), new {id = dtoFromRequest.Id});
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _shippingTypeService.DeleteAsync(id);
                TempData["Deleted Successfully"] = true;
            }
            catch
            {
                TempData["Deleted Successfully"] = false;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
