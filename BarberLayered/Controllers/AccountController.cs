using BarberLayered.Models;
using BuinessLogicLayer.DTOs;
using BuinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using static BarberLayered.Models.RegisterViewModel;

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

        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            int result = await _loginService.Login(loginModel.Email, loginModel.Password);

            switch (result)
            {
                case -1: // Not found
                    TempData["ErrorMessage"] = "Invalid email or password.";
                    return View(loginModel);
                case 1: // Client
                    return RedirectToAction("Index", "BarberShop");
                case 2: // Barber
                    return RedirectToAction("Index", "Barbers");
                case 3: // Admin
                    return RedirectToAction("Index", "BarberService");
                default:
                    return View(loginModel);
            }
        }

        //// GET: /Account/Login
        //public IActionResult Login()
        //{
        //    // Перевіряємо, чи є TempData["ErrorMessage"] та передаємо його до ViewData
        //    var errorMessage = TempData["ErrorMessage"] as string;
        //    if (!string.IsNullOrEmpty(errorMessage))
        //    {
        //        // Передаємо errorMessage до представлення
        //        ViewData["ErrorMessage"] = errorMessage;
        //        // Опційно видаляємо запис TempData
        //        TempData.Remove("ErrorMessage");
        //    }
        //    return View();
        //}


        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }


        //POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModelWithKey registerViewModel)
        {
            if (!registerViewModel.Password.Equals(registerViewModel.ConfirmPassword))
            {
                TempData["ErrorMessage"] = "The password and confirmation password do not match.";
                return View(registerViewModel);
            }

            int result;
            switch (registerViewModel.UserType)
            {
                case UserType.Barber:
                    //if (registerViewModel is RegisterViewModelWithKey)
                    //{
                    //    var barberDto = new BarberDto()
                    //    {
                    //        Name = registerViewModel.FirstName,
                    //        Surname = registerViewModel.LastName,
                    //        Phone = registerViewModel.Phone,
                    //        Email = registerViewModel.Email,
                    //        PasswordHash = registerViewModel.Password,
                    //        Key = ((RegisterViewModelWithKey)registerViewModel).RegistrationKey
                    //    };
                    //    result = await _registerService.BarberRegister(barberDto);
                    //}
                    //else
                    //{
                    //    throw new ArgumentException("Registration Key is required for Barber registration.");
                    //}
                    break;
                case UserType.Admin:
                    // result = await _registerService.AdminRegister((adminDto);
                    break;
                default:
                    var newClient = new ClientDto()
                    {
                        Name = registerViewModel.FirstName,
                        Surname = registerViewModel.LastName,
                        Phone = registerViewModel.Phone,
                        Email = registerViewModel.Email,
                        PasswordHash = registerViewModel.Password
                    };
                    result = await _registerService.Register(newClient);

                    break;
            }

            //if (result == -1) // Client with such email already exists
            //{
            //    TempData["ErrorMessage"] = "A user with this email already exists.";
            //    return View(registerViewModel);
            //}


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
