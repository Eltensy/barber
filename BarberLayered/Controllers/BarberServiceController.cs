using BuinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;


namespace BarberLayered.Controllers
{
    public class BarberServiceController : Controller
    {
        private List<BarberLayered.Models.Service> _services;
        private BarberLayered.Models.Barber? _barber;
        private readonly IServiceService _serviceService;
        private readonly IBarberService _barberService;


        public BarberServiceController(IServiceService serviceService, IBarberService barberService)
        {
            _serviceService = serviceService;
            _barberService = barberService;
            _barber = null;
        }

        public async Task<IActionResult> Index(int id)
        {
            var barber = await _barberService.GetBarberById(id);
            if (barber == null) 
            {
                throw new Exception();
            }
            else
            {
                _barber = new Models.Barber()
                {
                    Id = barber.Id,
                    Name = barber.Name,
                    Surname = barber.Surname,
                    Phone = barber.Phone,
                    Email = barber.Email,
                    PasswordHash = barber.PasswordHash,
                    PhotoUri = barber.PhotoUri,
                    Description = barber.Description,
                    PortfolioUri = barber.PortfolioUri,
                };
            }
            
            var services = await _serviceService.GetServicesByBarberId(id);
            if (!services.Any()) // No services for this barber
            {
                throw new Exception();
            }
            else
            {
                _services = new List<Models.Service>();
                foreach (var service in services)
                {
                    _services.Add(new Models.Service()
                    {
                        Id = service.Id,
                        fk_BarberId = service.Id,
                        Title = service.Title,
                        Description = service.Description,
                        Duration = service.Duration,
                        Price = service.Price,
                    });
                }
            }

            // Фільтруємо послуги за id барбера
            //var barberServices = _services.FindAll(service => service.fk_BarberId == id);

            // Передаємо список послуг у в`ю
            ViewBag.Barber = _barber;
            ViewBag.Services = _services;

            return View();
        }
    }
}
