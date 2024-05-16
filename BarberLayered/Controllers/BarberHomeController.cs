using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BarberLayered.Controllers
{
    [Authorize(Roles = "Barber")]
    public class BarberHomeController : Controller
    {
        private readonly IBarberHomeService _barberHomeService;
        private readonly IBarberService _barberService;
        private Models.Barber? _barber;
        private readonly List<Models.Visit> _visits;

        public BarberHomeController(IBarberHomeService barberHomeService, IBarberService barberService)
        {
            _barberHomeService = barberHomeService;
            _barberService = barberService;
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

            return View(_barber);
        }

        public async Task<IActionResult> EditProfile(int barberId)
        {
            var barber = await _barberService.GetBarberById(barberId);

            if (barber == null)
            {
                Log.Error("No info about Barber with id={Id} was found in the DataBase", barberId);
            }
            else
            {
                _barber = new Barber(barber);
            }

            ViewBag.Barber = _barber;
            return View(_barber);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBarber(Barber barber)
        {
            if (ModelState.IsValid)
            {
                BarberDto barberDto = new BarberDto()
                {
                    Id = barber.Id,
                    Name = barber.Name,
                    Surname = barber.Surname,
                    Phone = barber.Phone,
                    Email = barber.Email,
                };

                try
                {
                    // Update the barber
                    await _barberService.UpdateBarber(barberDto);

                    TempData["SuccessMessage"] = "Your data has been successfully updated!";
                }
                catch (Exception ex)
                {
                    // Handling errors during data update
                    TempData["ErrorMessage"] = "Incorrectly entered data";
                }
            }
            else
            {
                //TempData["ErrorMessage"] = "Incorrectly entered data";
            }

            return RedirectToAction("EditProfile", new { barberId = barber.Id });
        }

    }
}