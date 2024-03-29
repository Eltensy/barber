using BuinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace BarberLayered.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ILoginService _loginService;
        //private readonly IRegisterService _registerService;

        public AccountController(ILogger<AccountController> logger, ILoginService loginService, IRegisterService registerService)
        {
            _logger = logger;
            _loginService = loginService;
            //_registerService = registerService;
        }
        // GET: /Account/Index
        public IActionResult Index()
        {
            _logger.LogInformation("Visited the Account Index page");
            return View();
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            _logger.LogInformation("Visited Login page");
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            int result = _loginService.Login(email, password);
            if (result == 0)
            {
                _logger.LogInformation("User typed wrong password!");
                throw new Exception("Wrong password!");
            }
            else if (result == 1)
            {
                _logger.LogInformation("Client logged in successfully!");
                return RedirectToAction("Register");
            }
            else if (result == 2)
            {
                _logger.LogInformation("Barber logged in!");
                throw new Exception("Barber logged in!");
            }
            else
            {
                _logger.LogInformation("LoginSevice returned -1!");
                throw new Exception();
            }
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            _logger.LogInformation("Visited Register page");
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        //public IActionResult Register(string firstName, string lastName, string phone, string email, string password, string confirmPassword)
        //{
        //    if (!password.Equals(confirmPassword))
        //    {
        //        _logger.LogInformation("Passowrd and confirmPassword are not equal!");
        //        throw new Exception();
        //    }
        //    int result = _registerService.Register(firstName, lastName, phone, email, password);
            
        //    if (result == 0) // Client signed up successfully
        //    {
        //        _logger.LogInformation("User signed up successfully!");
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        _logger.LogInformation("RegisterSevice returned -1!");
        //        throw new Exception();
        //    }

        //}

        // POST: /Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            _logger.LogInformation("Logout!");
            return RedirectToAction("Index", "BarberShop");
        }
    }
}
