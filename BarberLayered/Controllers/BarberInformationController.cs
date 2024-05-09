using BarberLayered.Models;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using BusinessLogicLayer.DTOs;
using System.Net;

namespace BarberLayered.Controllers
{
    public class BarberInformationController : Controller
    {
        private readonly IBarberService _barberService;
        private readonly IReviewService _reviewService;
        private List<BarberLayered.Models.Review> _reviews;
        private Models.Barber? _barber;

        public BarberInformationController(IBarberService barberService, IReviewService reviewService)
        {
            _barberService = barberService;
            _reviewService = reviewService;
            _barber = null;
            _reviews = new List<Models.Review>();
        }

        public async Task<IActionResult> Index(int id)
        {
            var barber = await _barberService.GetBarberById(id);
            if (barber == null)
            {
                Log.Error("No info about Barber with id={Id} was found in the DataBase", id);
            }
            else
            {
                _barber = new Barber(barber);
            }
            
            var reviews = await _reviewService.GetReviewsByBarberId(id);
            if (!reviews.Any()) // No reviews logic for the view
            {
                Log.Information("No reviews for the Barber with id={Id} was found in the DataBase", id);
            }
            else
            {
                foreach (var review in reviews)
                {
                    _reviews.Add(new Review(review));
                }
            }

            ViewBag.Barber = _barber;
            ViewBag.Reviews = _reviews;

            return View();
        }

        public async Task<IActionResult> BarberInformationClient(int id, int clientId)
        {

            var barber = await _barberService.GetBarberById(id);
            if (barber == null)
            {
                Log.Error("No info about Barber with id={Id} was found in the DataBase", id);
            }
            else
            {
                _barber = new Barber(barber);
            }

            var reviews = await _reviewService.GetReviewsByBarberId(id);
            if (!reviews.Any()) // No reviews logic for the view
            {
                Log.Information("No reviews for the Barber with id={Id} was found in the DataBase", id);
            }
            else
            {
                foreach (var review in reviews)
                {
                    _reviews.Add(new Review(review));
                }
            }

            ViewBag.Barber = _barber;
            ViewBag.Reviews = _reviews;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(int barberId, string reviewerName, string comment, int rating,int clientId)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
               
                TempData["ErrorMessage"] = "Error: Your review is empty ";
                return RedirectToAction("BarberInformationClient", new { id = barberId });
            }

            var reviewDto = new ReviewDto
            {
                fk_BarberId = barberId,
                fk_ClientId = clientId,
                Text = comment,
                Rating = rating, 
                Date = DateTime.UtcNow
            };

            await _reviewService.InsertReview(reviewDto);

            return RedirectToAction("BarberInformationClient", new { id = barberId });
        }


    }
}
