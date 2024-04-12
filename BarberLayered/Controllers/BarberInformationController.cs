using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            
            var reviews = await _reviewService.GetReviewsByBarberId(id);
            if (!reviews.Any()) // No reviews logic for the view
            {
                Log.Information("No reviews for the Barber with id={Id} was found in the DataBase", id);
            }
            else
            {
                foreach (var review in reviews)
                {
                    _reviews.Add(new Models.Review()
                    {
                        Id = review.Id,
                        fk_BarberId = review.fk_BarberId,
                        fk_ClientId = review.fk_ClientId,
                        Text = review.Text,
                        Rating = review.Rating,
                        Date = review.Date,
                    });
                }
            }
            

            ViewBag.Barber = _barber;
            ViewBag.Reviews = _reviews;

            return View();
        }
    }
}
