using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BarberLayered.Controllers
{
    public class BarberHomeController : Controller
    {
        private readonly IBarberService _barberService;
        private readonly IBarberHomeService _barberHomeService;
        private Models.Barber? _barber;
        private readonly List<Models.Visit> _visits;

        public BarberHomeController(IBarberService barberService, IBarberHomeService barberHomeService)
        {
            _barberService = barberService;
            _barberHomeService = barberHomeService;
            _barber = null;
            _visits = new List<Visit>();
        }

        public async Task<IActionResult> Index(int barberId)
        {

            var barber = await _barberService.GetBarberById(barberId);
            if( barber == null )
            {
                Log.Error("No Barber with id={Id} information in DataBase", barberId);
            }
            else
            {
                _barber = new Barber(barber);
            }

            var visits = await _barberHomeService.GetVisitsByBarberId(barberId);
            if (!visits.Any())
            {
                Log.Error("No visits for barber id={Id} in DataBase", barberId);
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