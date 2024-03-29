using Microsoft.AspNetCore.Mvc;
using BarberLayered.Models;

namespace BarberLayered.Controllers
{
    public class BarbersController : Controller
    {
        private readonly List<Barber> _barbers;
        public BarbersController()
        {
            _barbers = new List<Barber>
            {
                new Barber { Id = 1, Name = "John", Surname = "Doe", Phone = "1234567890", Email = "john@example.com", Password = "password", PhotoUri = "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=", Description = "Experienced barber with 10+ years of experience.", PortfolioUri = "portfolio/john" },
                new Barber { Id = 2, Name = "Jake", Surname = "Smith", Phone = "0987654321", Email = "jake@example.com", Password = "password", PhotoUri = "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=", Description = "Creative barber specializing in modern hairstyles.", PortfolioUri = "portfolio/jane" },
            };
        }

        public IActionResult Index()
        {
            return View(_barbers);
        }
    }
}
