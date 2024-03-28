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
        public IActionResult Login(string email, string password)
        {
            IClientRepository _clientRepository = new ClientRepository(new DataAccessLayer.Data.DataContext());
            //IBarberRepository _barberRepository = new BarberRepository(new DataAccessLayer.Data.DataContext());
            

            var client = _clientRepository.GetClients().First(x => x.Email == email);

            HashAlgorithm sha = SHA256.Create();
            var bytes = Encoding.ASCII.GetBytes(password);
            byte[] hashedPassword;
            hashedPassword = sha.ComputeHash(bytes);

            if (client != null)
            {
                if (client.Password.Equals(Encoding.ASCII.GetString(hashedPassword)))
                {
                    return RedirectToAction("Register");
                }
                else
                    throw new Exception();

            }
            else
            {
                //var barber = _barberRepository.GetBarbers().First(x => x.Email == email);
                //if (barber != null)
                //{
                //    if (barber.Password.Equals(Encoding.ASCII.GetString(hashedPassword)))
                //        result = 0;
                //    else
                //        result = 2;
                //}
                throw new Exception();
            }
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string phone, string email, string password, string confirmPassword)
        {
            IRegisterService _registerService = new RegisterService(new DataAccessLayer.Interfaces.ClientRepository(new DataAccessLayer.Data.DataContext()));

            if (!password.Equals(confirmPassword))
            {
                throw new Exception();
            }
            int result = _registerService.Register(firstName, lastName, phone, email, password);
            
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
