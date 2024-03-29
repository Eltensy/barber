using BuinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class LoginController : Controller
    {
        private ILoginService _loginService;
        public LoginController(ILogger<LoginController> logger)
        {
            _loginService = new LoginService(new ClientRepository(new DataAccessLayer.Data.DataContext()), new BarberRepository(new DataAccessLayer.Data.DataContext()));
        }

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Login() 
        //{
        //    string email = "rostyk2004@gmail.com";
        //    string password = "12345";
        //    int result = _loginService.Login(email, password);
        //    if(result == 1) // Client authorization is successful
        //    {
                
        //    }
        //    else if(result == 2) // Barber authorization is successful
        //    {
                
        //    }
        //    else if (result == 0) // Wrong password
        //    {

        //    }
        //    else // Client or barber doesn't exist
        //    {

        //    }
        //}
    }
}
