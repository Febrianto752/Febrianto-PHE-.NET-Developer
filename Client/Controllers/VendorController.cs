using Client.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Newtonsoft.Json;
using Utilities.Enums;

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
        public async Task<IActionResult> Index(string status = nameof(VendorStatusEnum.Accepted))
        {
            List<Vendor>? list = new();

            ResponseVM? response = await _vendorService.GetAllVendorsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Vendor>>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid guid)
        {
            Vendor vendor = new();

            ResponseVM? response = await _vendorService.GetVendorByGuid(guid);

            if (response != null && response.IsSuccess)
            {
                vendor = JsonConvert.DeserializeObject<Vendor>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(vendor);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("{guid}/approve-by-admin")]
        public async Task<IActionResult> ApproveByAdmin(Guid guid)
        {
            Vendor vendor = new();

            ResponseVM? response = await _vendorService.ApproveByAdmin(guid);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Vendor disetujui untuk bergabung oleh admin";

            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost("{guid}/approve-by-manager")]
        public async Task<IActionResult> ApproveByManager(Guid guid)
        {
            Vendor vendor = new();

            ResponseVM? response = await _vendorService.ApproveByManager(guid);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Vendor disetujui untuk bergabung oleh manager logistik";

            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return RedirectToAction("Index");
        }


        [HttpPost("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            ResponseVM? response = await _vendorService.DeleteVendor(guid);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "vendor deleted successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = response?.Message;
            }
            return RedirectToAction("Index");
        }



        //[HttpPost]
        //public async Task<IActionResult> Create(CreateVendorVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ResponseVM? response = await _vendorService.CreateVendorAsync(model);

        //        if (response != null && response.IsSuccess)
        //        {
        //            TempData["Success"] = "Vendor created successfully";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            TempData["Error"] = response?.Message;
        //        }
        //    }
        //    return View(model);
        //}
    }


}
