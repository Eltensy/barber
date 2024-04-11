using Microsoft.AspNetCore.Mvc;
using BarberLayered.Models;

namespace BarberLayered.Controllers
{
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            var barberShop = new BarberShop
            {
                Id = 1,
                Name = "Example Barber Shop",
                Address = "123 Example St",
                Phone = "+1234567890",
                Description = "Our barber shop is a modern establishment where each client receives personalized service from professional barbers. Located in the city center, we offer a wide range of services, from haircuts and shaves to beard and mustache care. Our team consists of experienced masters who are always ready to meet your needs in style and grooming."
            };

            var admin = new Admin
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Phone = "+1234567890",
                Email = "john@example.com"
            };

            ViewBag.Admin = admin;

            return View(barberShop);
        }

    }
}
