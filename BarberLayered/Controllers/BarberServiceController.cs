using Microsoft.AspNetCore.Mvc;
using BarberLayered.Models;
using DataAccessLayer.Entities;

namespace BarberLayered.Controllers
{
    public class BarberServiceController : Controller
    {
        private readonly List<BarberLayered.Models.Service> _services;

        public BarberServiceController()
        {
            _services = new List<BarberLayered.Models.Service>
            {
                new BarberLayered.Models.Service { Id = 1, fk_BarberId = 1, Title = "Haircut", Description = "Basic haircut", Duration = new TimeOnly(0, 30), Price = 400 },
                new BarberLayered.Models.Service { Id = 2, fk_BarberId = 1, Title = "Beard Trim", Description = "Trim and shape beard", Duration = new TimeOnly(0, 15), Price = 200 },
                new BarberLayered.Models.Service { Id = 3, fk_BarberId = 1, Title = "Shave", Description = "Traditional straight razor shave", Duration = new TimeOnly(0, 30), Price = 150 },
                new BarberLayered.Models.Service { Id = 4, fk_BarberId = 2, Title = "Haircut", Description = "Basic haircut", Duration = new TimeOnly(0, 30), Price = 450 }
            };
        }

        public IActionResult Index(int id)
        {
            var barber = new BarberLayered.Models.Barber
            {
                Id = id,
                Name = "John",
                Surname = "Doe",
                Phone = "1234567890",
                Email = "john@example.com",
                Password = "password",
                PhotoUri = "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=",
                Description = "Experienced barber with 10+ years of experience. Specializes in classic and modern hairstyles. Always committed to providing the best service and ensuring customer satisfaction.",
                PortfolioUri = "portfolio/john"
            };

            // Фільтруємо послуги за id барбера
            var barberServices = _services.FindAll(service => service.fk_BarberId == id);

            // Передаємо список послуг у в`ю
            ViewBag.Barber = barber;
            ViewBag.Services = barberServices;

            return View();
        }
    }
}
