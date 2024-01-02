using Client.Models;
using Client.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var username = User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            ViewData["username"] = username;



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
