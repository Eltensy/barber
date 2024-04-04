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
                case 0:
                    return RedirectToAction("Login");
                case 1:
                    return RedirectToAction("Index", "BarberShop");
                case 2:
                    return RedirectToAction("Index", "Barbers");
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
            
            if (result == 0) // Client with such email already exists
            {
                return RedirectToAction("Login");
            }
            else
            {
                throw new Exception();
            }
        }

        // POST: /Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {

            return RedirectToAction("Index", "BarberShop");
        }
    }
}
