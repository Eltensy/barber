using BarberLayered.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class BarberHomeController : Controller
    {
        public IActionResult Index()
        {
            var barber = new Barber 
            
            {
                Name = "Олег",
                Surname = "Леськів",
                Phone = "0504567890",
                Email = "olegles@example.com",
                PasswordHash = "password",
                PhotoUri = "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=",
                Description = "Досвідчений барбер з 10-річним стажем роботи. Спеціалізується в класичних та сучасних стрижках. Завжди готовий надати найкращий сервіс.",
                PortfolioUri = "portfolio/john"
            };

            return View(barber); 
        }
    }
}