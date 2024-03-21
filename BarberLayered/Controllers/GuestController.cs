using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
