using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;

        public AccountController(
            ILoginService loginService,
            IRegisterService registerService)
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
            var result = await _loginService.Login(loginModel.Email, loginModel.Password);
            if (result.ErrorMsg != "")
            {
                TempData["ErrorMessage"] = result.ErrorMsg;
                return View(loginModel);
            }

            switch (result.UserType)
            {
                case _UserType.Admin:
                    return RedirectToAction("Index", "AdminHome",
                        new Admin(result));
                case _UserType.Barber:
                    return RedirectToAction("Index", "BarberHome",
                        new Barber(result));
                case _UserType.Client:
                    return RedirectToAction("Index", "BarberShop");
                default:
                    return View(loginModel);
            }
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModelWithKey registerViewModel)
        {
            if (!registerViewModel.Password.Equals(registerViewModel.ConfirmPassword))
            {
                TempData["ErrorMessage"] = "The password and confirmation password do not match.";
                return View(registerViewModel);
            }

            UserExtDto result;
            RegistrationDto registrationDto = new RegistrationDto()
            {
                RegistrationKey = registerViewModel.RegistrationKey,
                Name = registerViewModel.FirstName,
                Surname = registerViewModel.LastName,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                Phone = registerViewModel.Phone,
                UserType = (_UserType)registerViewModel.UserType
            };

            result = await _registerService.Register(registrationDto);
            if (result.ErrorMsg != "")
            {
                TempData["ErrorMessage"] = result.ErrorMsg;
                return View(registerViewModel);
            }

            switch (result.UserType)
            {
                case _UserType.Admin:
                    return RedirectToAction("Index", "AdminHome",
                        new Admin(result));
                case _UserType.Barber:
                    return RedirectToAction("Index", "BarberHome",
                        new Barber(result));
                case _UserType.Client:
                    return RedirectToAction("Index", "BarberShop");
                default:
                    return RedirectToAction("Index", "BarberShop");
            }
        }

        // POST: /Account/Logout
        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            // User session close logic
            return RedirectToAction("Index", "BarberShop");
        }
    }
}
