using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
