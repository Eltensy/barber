using BuinessLogicLayer.DTOs;
using BuinessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

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
                    return RedirectToAction("User");
                case 2:
                    return RedirectToAction("Home");
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
                Password = password 
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

            //return RedirectToAction("Login");
        }

        // POST: /Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {

            return RedirectToAction("Index", "BarberShop");
        }
    }
}
