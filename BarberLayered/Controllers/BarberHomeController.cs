using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BarberLayered.Controllers
{
    public class BarberHomeController : Controller
    {
        private readonly IBarberHomeService _barberHomeService;
        private Models.Barber? _barber;
        private readonly List<Models.Visit> _visits;

        public BarberHomeController(IBarberHomeService barberHomeService)
        {
            _barberHomeService = barberHomeService;
            _barber = null;
            _visits = new List<Visit>();
        }

        public async Task<IActionResult> Index(Barber barber)
        {
            _barber = barber;

            var visits = await _barberHomeService.GetVisitsByBarberId(_barber.Id);
            if (!visits.Any())
            {
                Log.Error("No visits for barber id={Id} in DataBase", _barber.Id);
            }
            else
            {
                foreach (var visit in visits)
                {
                    _visits.Add(new Visit(visit));
                }
            }

            ViewBag.Barber = _barber;
            ViewBag.Visits = _visits;

            return View();
        }
    }
}