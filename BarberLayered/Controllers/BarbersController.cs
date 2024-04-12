using BarberLayered.Models;
using BuinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class BarbersController : Controller
    {
        private readonly List<Barber> _barbers;
        public BarbersController()
        {
            _barbers = new List<Barber>
            {
                new Barber { Id = 1, Name = "Олег", Surname = "Леськів", Phone = "0504567890", Email = "olegles@example.com", PasswordHash = "password", PhotoUri = "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=", Description = "Досвідчений барбер з 10-річним стажем роботи.", PortfolioUri = "portfolio/john" },
                new Barber { Id = 2, Name = "Роман", Surname = "Мигота", Phone = "0987654321", Email = "romanmygota@example.com", PasswordHash = "password", PhotoUri = "https://media.istockphoto.com/id/506514230/photo/beard-grooming.jpg?s=612x612&w=0&k=20&c=QDwo1L8-f3gu7mcHf00Az84fVU8oNpQLgvUw6eGPEkc=", Description = "Креативний барбер, працює з класичними стрижками.", PortfolioUri = "portfolio/jane" },
            };
        }

        public IActionResult Index()
        {
            return View(_barbers);
        }
    }
}