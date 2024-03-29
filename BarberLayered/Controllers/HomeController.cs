using BarberLayered.Filters;
using BarberLayered.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarberLayered.Controllers
{
    //[ServiceFilter(typeof(LogActionFilter))]
    [ServiceFilter(typeof(LogActionFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Visited the Home Index page");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Visited Privacy page");
            return View();
        }

        public IActionResult Registration()
        {
            _logger.LogInformation("Visited Registration page");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogError($"Error occurred. RequestId: {errorId}");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
