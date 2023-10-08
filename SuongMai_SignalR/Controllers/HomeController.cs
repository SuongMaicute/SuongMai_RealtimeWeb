using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuongMai_SignalR.Hubs;
using SuongMai_SignalR.Models;
using System.Diagnostics;

namespace SuongMai_SignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub;
        public HomeController(ILogger<HomeController> logger,
            IHubContext<DeathlyHallowsHub> deathlyHub)
        {
            _logger = logger;
            _deathlyHub = deathlyHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DealthyHallowRace.ContainsKey(type))
            {
                SD.DealthyHallowRace[type]++;
            }
            await _deathlyHub.Clients.All.SendAsync("updateDealthyHallowsCount",
                SD.DealthyHallowRace[SD.Cloak],
                SD.DealthyHallowRace[SD.Stone],
                SD.DealthyHallowRace[SD.Wand]
                );
            return Accepted();
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