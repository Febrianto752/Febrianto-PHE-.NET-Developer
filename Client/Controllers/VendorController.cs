using Client.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Models.ViewModels.Vendor;

namespace Client.Controllers
{
    public class VendorController : Controller
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVendorVM model)
        {
            if (ModelState.IsValid)
            {
                ResponseVM? response = await _vendorService.CreateVendorAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Vendor created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = response?.Message;
                }
            }
            return View(model);
        }
    }


}
