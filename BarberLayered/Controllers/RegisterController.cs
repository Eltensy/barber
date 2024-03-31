using BuinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Register()
        //{
        //    string name = "Rostyk";
        //    string surname = "Stets";
        //    string phone = "0960000000";
        //    string email = "rostyk2004@gmail.com";
        //    string password = "12345";

        //    int result = _registerService.Register(name, surname, phone, email, password);
        //    if (result == 1) // Client registration is successful
        //    {
        //        return View();
        //    }

        //    else if (result == 0) // Client with such email already exists
        //    {

        //    }
        //    else 
        //    {

        //    }
        //}
    }
}
