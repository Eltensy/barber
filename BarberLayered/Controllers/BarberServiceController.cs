using BarberLayered.Models;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;


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
                Log.Error("No Barber with id={Id} information in DataBase", id);
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
                Log.Error("No Services for Barber with id={Id} was found in DataBase", id);
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

            ViewBag.Barber = _barber;
            ViewBag.Services = _services;

            return View();
        }

        public async Task<IActionResult> Add(int id)
        {
            var barber = await _barberService.GetBarberById(id);
            if (barber == null)
            {
                Log.Error("No Barber with id={Id} information in DataBase", id);
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
                Log.Error("No Services for Barber with id={Id} was found in DataBase", id);
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

            ViewBag.Barber = _barber;
            ViewBag.Services = _services;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetServices(int id)
        {

            var services = await _serviceService.GetServicesByBarberId(id);
            if (!services.Any()) // No services for this barber
            {
                Log.Error("No Services for Barber with id={Id} was found in DataBase", id);
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
            var barberServices = _services;
            Console.WriteLine(id);
            return Json(new { barberServices });
        }

        [HttpPost]
        public IActionResult SubmitAppointment(AppointmentViewModel model)
        {
            System.Console.WriteLine("AppointmentViewModel:");
            System.Console.WriteLine(model.Name + " " + model.Phone + " " + model.SelectedDay + " " + model.SelectedTime + " " + model.SelectedBarberId + " " + model.SelectedServiceId);

            return RedirectToAction("Index", "Home");
        }
    }
}