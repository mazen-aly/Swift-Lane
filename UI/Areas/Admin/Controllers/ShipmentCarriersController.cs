using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,Viewer")]
    public class ShipmentCarriersController : Controller
    {
        private readonly IShipmentCarrierService _shipmentCarrierService;

        public ShipmentCarriersController(IShipmentCarrierService shipmentCarrierService)
        {
            _shipmentCarrierService = shipmentCarrierService;
        }


        public async Task<IActionResult> Index()
        {
            var allShipmentCarriers = await _shipmentCarrierService.GetAllAsync();
            return View(allShipmentCarriers);
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
        public async Task<IActionResult> Create(ShipmentCarrierDto dtoFromRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(dtoFromRequest);
            }

            try
            {
                TempData["Created Successfully"] = await _shipmentCarrierService.AddNewAsync(dtoFromRequest);
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
            var shipmentCarrierDto = await _shipmentCarrierService.GetByIdAsync(id);

            if (shipmentCarrierDto is null)
            {
                TempData["Error"] = true;

                return RedirectToAction(nameof(Index));
            }

            return View(shipmentCarrierDto);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ShipmentCarrierDto dtoFromRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(dtoFromRequest);
            }

            try
            {
                TempData["Updated Successfully"] = await _shipmentCarrierService.UpdateAsync(dtoFromRequest);
            }
            catch
            {
                TempData["Updated Successfully"] = false;
            }

            return RedirectToAction(nameof(Update), new { id = dtoFromRequest.Id });
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _shipmentCarrierService.DeleteAsync(id);
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
