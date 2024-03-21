using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
