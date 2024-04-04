using BuinessLogicLayer.DTOs;
using BuinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;

        public AccountController(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        // GET: /Account/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            int result = await _loginService.Login(email, password);

            switch (result)
            {
                case -1: // Not found
                    return RedirectToAction("Login");
                case 1: // Client
                    return RedirectToAction("Index", "BarberShop");
                case 2: // Barber
                    return RedirectToAction("Index", "Barbers");
                case 3: // Admin
                    return RedirectToAction("Index", "BarberService");
                default:
                    return View();

            }
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(string firstName, string lastName, string phone, string email, string password, string confirmPassword)
        {
            if (!password.Equals(confirmPassword))
            {
                throw new Exception();
            }

            var newClient = new ClientDto()
            {
                Name = firstName,
                Surname = lastName,
                Phone = phone,
                Email = email,
                PasswordHash = password
            };
            int result = await _registerService.Register(newClient);
            if (result == -1) // Client with such email already exists
            {
                throw new Exception();
            }

            return RedirectToAction("Index", "BarberShop");
        }

        // POST: /Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            // User session close logic
            return RedirectToAction("Index", "BarberShop");
        }
    }
}
