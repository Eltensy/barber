using Microsoft.AspNetCore.Mvc;
using barber.Models;
using System;
using System.Collections.Generic;

namespace barber.Controllers
{
    public class BarberInformationController : Controller
    {
        private readonly List<Review> _reviews;

        public BarberInformationController()
        {

            _reviews = new List<Review>
            {
                new Review { Id = 1, fk_ClientId = 1, fk_BarberId = 1, Text = "Great service, highly recommend!", Rating = 4.5f, Date = new DateTime(2024, 3, 20) },
                new Review { Id = 2, fk_ClientId = 2, fk_BarberId = 1, Text = "Very talented barber, always satisfied with the haircut.", Rating = 5.0f, Date = new DateTime(2024, 3, 18) },
                new Review { Id = 3, fk_ClientId = 3, fk_BarberId = 1, Text = "Professional and friendly, will definitely come back.", Rating = 4.7f, Date = new DateTime(2024, 3, 15) },
                
                new Review { Id = 1, fk_ClientId = 1, fk_BarberId = 2, Text = "Great service, highly recommend!", Rating = 4.5f, Date = new DateTime(2024, 3, 20) },
                new Review { Id = 2, fk_ClientId = 2, fk_BarberId = 2, Text = "Very talented barber, always satisfied with the haircut.", Rating = 5.0f, Date = new DateTime(2024, 3, 18) },

                new Review { Id = 3, fk_ClientId = 3, fk_BarberId = 3, Text = "Professional and friendly, will definitely come back.", Rating = 4.7f, Date = new DateTime(2024, 3, 15) }
            };
        }

        public IActionResult Index(int id)
        {

            var barber = new Barber
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

            // Фільтруємо відгуки за id барбера
            var barberReviews = _reviews.FindAll(review => review.fk_BarberId == id);

            // Передаємо дані барбера та список відгуків у в`ю
            ViewBag.Barber = barber;
            ViewBag.Reviews = barberReviews;

            return View();
        }
    }
}
