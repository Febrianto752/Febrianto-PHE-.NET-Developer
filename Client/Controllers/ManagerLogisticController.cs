using Client.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Models.ViewModels.Account;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class ManagerLogisticController : Controller
    {

        private readonly IManagerLogisticService _managerLogisticService;

        public ManagerLogisticController(ILogger<HomeController> logger, IManagerLogisticService managerLogisticService)
        {

            _managerLogisticService = managerLogisticService;
        }
        public async Task<IActionResult> Index()
        {
            List<GetAccountVM>? list = new();

            ResponseVM? response = await _managerLogisticService.GetAllManagerLogisticsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<GetAccountVM>>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(list);
        }
    }
}
