using Microsoft.AspNetCore.Mvc;


namespace BarberLayered.Controllers
{
    public class BarberServiceController : Controller
    {
        private readonly List<BarberLayered.Models.Service> _services;

        public BarberServiceController()
        {
            _services = new List<BarberLayered.Models.Service>
            {
                new BarberLayered.Models.Service { Id = 1, fk_BarberId = 1, Title = " Стрижка", Description = "Звичайна стрижка", Duration = new TimeOnly(0, 30), Price = 400 },
                new BarberLayered.Models.Service { Id = 2, fk_BarberId = 1, Title = " Стрижка бороди", Description = "Стрижка бороди,надання форми", Duration = new TimeOnly(0, 15), Price = 200 },
                new BarberLayered.Models.Service { Id = 3, fk_BarberId = 1, Title = "Гоління", Description = "Традиційне гоління бритвою", Duration = new TimeOnly(0, 30), Price = 150 },
                new BarberLayered.Models.Service { Id = 4, fk_BarberId = 2, Title = "Стрижка", Description = "Звичайна стрижка", Duration = new TimeOnly(0, 30), Price = 450 }
            };
        }

        public IActionResult Index(int id)
        {
            var barber = new BarberLayered.Models.Barber
            {
                Id = 1,
                Name = "Олег",
                Surname = "Леськів",
                Phone = "0504567890",
                Email = "olegles@example.com",
                PasswordHash = "password",
                PhotoUri =
                    "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=",
                Description =
                    "Досвідчений барбер з 10-річним стажем роботи. Спеціалізується в класичних та сучасних стрижках. Завжди готовий надати найкращий сервіс.",
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
