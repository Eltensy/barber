using BarberLayered.Models;
using BuinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BarberLayered.Controllers
{
    public class BarbersController : Controller
    {
        private List<Barber> _barbers;
        private readonly IBarberService _barberService;

        public BarbersController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        public async Task<IActionResult> Index()
        {
            var barbers = await _barberService.GetBarbers();
            if (!barbers.Any()) // No barbers in DB
            {
                Log.Error("No Barbers in DataBase");
            }
            else
            {
                _barbers = new List<Barber>();
                foreach (var barber in barbers)
                {
                    _barbers.Add(new Barber()
                    {
                        Id = barber.Id,
                        Name = barber.Name,
                        Surname = barber.Surname,
                        Description = barber.Description,
                        Email = barber.Email,
                        PasswordHash = barber.PasswordHash,
                        Phone = barber.Phone,
                        PhotoUri = barber.PhotoUri,
                        PortfolioUri = barber.PortfolioUri,
                    });
                }
            }
            
            return View(_barbers);
        }
    }
}
