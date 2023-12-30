using Client.Models;
using Client.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IManagerLogisticService _managerLogisticService;

        public HomeController(ILogger<HomeController> logger, IManagerLogisticService managerLogisticService)
        {
            _logger = logger;
            _managerLogisticService = managerLogisticService;
        }

        public async Task<IActionResult> Index()
        {



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
