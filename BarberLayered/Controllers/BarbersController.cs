using BarberLayered.Models;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class BarbersController : Controller
    {
        private readonly List<Barber> _barbers;
        private readonly IBarberService _barberService;

        public BarbersController(IBarberService barberService)
        {
            //_barbers = new List<Barber>
            //{
            //    new Barber { Id = 1, Name = "John", Surname = "Doe", Phone = "1234567890", Email = "john@example.com", PasswordHash = "password", PhotoUri = "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=", Description = "Experienced barber with 10+ years of experience.", PortfolioUri = "portfolio/john" },
            //    new Barber { Id = 2, Name = "Jake", Surname = "Smith", Phone = "0987654321", Email = "jake@example.com", PasswordHash = "password", PhotoUri = "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=", Description = "Creative barber specializing in modern hairstyles.", PortfolioUri = "portfolio/jane" },
            //};

            _barberService = barberService;
        }

        public IActionResult Index()
        {
            var barbers = await _barberService.GetBarbers();
            if (!barbers.Any()) // No barbers in DB
            {
                throw new Exception();
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
                    })
                }
            }
            
            return View(_barbers);
        }
    }
}
