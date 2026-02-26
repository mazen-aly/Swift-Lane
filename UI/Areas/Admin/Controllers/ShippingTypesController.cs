using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
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


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


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
                TempData["Created Successfully"] = await _shippingTypeService.AddNewAsync(dtoFromRequest, Guid.NewGuid());
            }
            catch
            {
                TempData["Created Successfully"] = false;
            }


            return RedirectToAction(nameof(Index));
        }


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
                TempData["Updated Successfully"] = await _shippingTypeService.UpdateAsync(dtoFromRequest, Guid.NewGuid());
            }
            catch
            {
                TempData["Updated Successfully"] = false;
            }

            return RedirectToAction(nameof(Update), new {id = dtoFromRequest.Id});
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _shippingTypeService.DeleteAsync(id, new Guid("3f9a8c6e-7b24-4d91-a8f2-5c1e9b7d42af"));
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
