using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            Log.Information("Login attempt with email: {Email}, password: {Password}", loginModel.Email, loginModel.Password);
            var result = await _loginService.Login(loginModel.Email, loginModel.Password);

            if (result == null)
            {
                TempData["ErrorMessage"] = "Invalid email or password.";
                return View(loginModel);
            }
            else
            {

                switch (result.UserType)
                {
                    case _UserType.Admin:
                        Admin admin = new Admin(result);
                        return RedirectToAction("Index", "AdminHome", admin);
                    case _UserType.Barber:
                        Barber barber = new Barber(result);
                        return RedirectToAction("Index", "BarberHome", barber);
                    case _UserType.Client:
                        return RedirectToAction("Index", "BarberShop");
                    default:
                        return View(loginModel);

                }
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
            Log.Information("Register attempt with email: {Email}, password: {Password}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.Password, registerViewModel.FirstName, registerViewModel.LastName);

            if (!registerViewModel.Password.Equals(registerViewModel.ConfirmPassword))
            {
                Log.Information("Register failed (passwords doesnt match) with email: {Email}, password: {Password}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.Password, registerViewModel.FirstName, registerViewModel.LastName);
                TempData["ErrorMessage"] = "The password and confirmation password do not match.";
                return View(registerViewModel);
            }

            UserExtDto? result = null;


            RegistrationDto registrationDto = new RegistrationDto()
            {
                RegistrationKey = registerViewModel.RegistrationKey,
                Name = registerViewModel.FirstName,
                Surname = registerViewModel.LastName,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                ConfirmPassword = registerViewModel.ConfirmPassword,
                Phone = registerViewModel.Phone
            };

            switch (registerViewModel.UserType)
            {
                case UserType.Admin:
                    registrationDto.UserType = _UserType.Admin;
                    break;
                case UserType.Barber:
                    registrationDto.UserType = _UserType.Barber;
                    break;
                default:
                    registrationDto.UserType = _UserType.Client;
                    break;
            }

            result = await _registerService.Register(registrationDto);
            if (result != null)
            {
                if (!result.IsRegistrationKeyValid)
                {
                    Log.Information("Register failed (registration key is wrong) with RegistrationKey: {RegistrationKey}", registerViewModel.RegistrationKey);
                    TempData["ErrorMessage"] = "The registration key is wrong.";
                    return View(registerViewModel);
                }
                else
                {
                    switch (result.UserType)
                    {
                        case _UserType.Admin:
                            Admin admin = new Admin(result);
                            return RedirectToAction("Index", "AdminHome", admin);
                        case _UserType.Barber:
                            Barber barber = new Barber(result);
                            return RedirectToAction("Index", "BarberHome", barber);
                        default:
                            return RedirectToAction("Index", "BarberShop");
                    }
                }
            }


            if (result == null)
            {
                TempData["ErrorMessage"] = "User with such email already exists.";
                Log.Information("Register failed with email: {Email}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.FirstName, registerViewModel.LastName);
                return View(registerViewModel);
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
