using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
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
                    return RedirectToAction("Index", "ClientHome", 
                        new Client(result));
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
                    return RedirectToAction("Index", "ClientHome",
                        new Client(result));
                default:
                    return RedirectToAction("Index", "BarberShop");
            }
        }

        // GET: /Account/ChangePassword
        public IActionResult ChangePassword()
        {
            return View();
        }


        // POST: /Account/ChangePassword
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Get the ID of the current user changing the password

                // Call the password change service

                // Success message for password change
                TempData["SuccessMessage"] = "Password has been changed successfully.";

            }
            catch (Exception ex)
            {
                // Handling errors during password change
                TempData["ErrorMessage"] = "An error occurred while changing the password: " + ex.Message;
            }

            return View(model);
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
