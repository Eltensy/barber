using BarberLayered.Models;
using BuinessLogicLayer.DTOs;
using BuinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BarberLayered.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly IRegistrationKeyService _registrationKeyService;

        public AccountController(ILoginService loginService, IRegisterService registerService, IRegistrationKeyService registrationKeyService)
        {
            _loginService = loginService;
            _registerService = registerService;
            _registrationKeyService = registrationKeyService;
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
            int result = await _loginService.Login(loginModel.Email, loginModel.Password);
            

            switch (result)
            {
                case -1: // Not found
                    TempData["ErrorMessage"] = "Invalid email or password.";
                    return View(loginModel);
                case 1: // Client
                    Log.Information("Successful logged in as client with email: {Email}, password: {Password}", loginModel.Email, loginModel.Password);
                    return RedirectToAction("Index", "BarberShop");
                case 2: // Barber
                    Log.Information("Successful logged in as barber with email: {Email}, password: {Password}", loginModel.Email, loginModel.Password);
                    //return RedirectToAction("Index", "Barbers");
                    return RedirectToAction("Index", "BarberHome");
                case 3: // Admin
                    Log.Information("Successful logged in as admin with email: {Email}, password: {Password}", loginModel.Email, loginModel.Password);
                    //return RedirectToAction("Index", "Barbers");
                    return RedirectToAction("Index", "AdminHome");
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
            Log.Information("Register attempt with email: {Email}, password: {Password}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.Password, registerViewModel.FirstName, registerViewModel.LastName);

            if (!registerViewModel.Password.Equals(registerViewModel.ConfirmPassword))
            {
                Log.Information("Register failed (passwords doesnt match) with email: {Email}, password: {Password}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.Password, registerViewModel.FirstName, registerViewModel.LastName);
                TempData["ErrorMessage"] = "The password and confirmation password do not match.";
                return View(registerViewModel);
            }

            int result = -1;

            var registrationKey = await _registrationKeyService.GetRegistrationKeyById(2);

            switch (registerViewModel.UserType)
            {
                case UserType.Barber:
                    if (registerViewModel.RegistrationKey.ToString().Equals(registrationKey.Key.ToString()))
                    {
                        var barberDto = new BarberDto()
                        {
                            Name = registerViewModel.FirstName,
                            Surname = registerViewModel.LastName,
                            Phone = registerViewModel.Phone,
                            Email = registerViewModel.Email,
                            PasswordHash = registerViewModel.Password,
                        };
                        result = await _registerService.BarberRegister(barberDto);
                        if(result == 0)
                        {
                            Log.Information("Successful register as barber with email: {Email}, password: {Password}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.Password, registerViewModel.FirstName, registerViewModel.LastName);
                            return RedirectToAction("Index", "Barbers");
                        }
                    }
                    break;
                case UserType.Admin:
                    if (registerViewModel.RegistrationKey.ToString().Equals(registrationKey.Key.ToString()))
                    {
                        var adminDto = new AdminDto()
                        {
                            Name = registerViewModel.FirstName,
                            Surname = registerViewModel.LastName,
                            Phone = registerViewModel.Phone,
                            Email = registerViewModel.Email,
                            PasswordHash = registerViewModel.Password,
                        };
                        result = await _registerService.AdminRegister(adminDto);
                        if (result == 0)
                        {
                            Log.Information("Successful register as admin with email: {Email}, password: {Password}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.Password, registerViewModel.FirstName, registerViewModel.LastName);
                            return RedirectToAction("Index", "BarberService");
                        }
                    }
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
                    if(result == 0) 
                    {
                            Log.Information("Successful register as client with email: {Email}, password: {Password}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.Password, registerViewModel.FirstName, registerViewModel.LastName);
                        return RedirectToAction("Index", "BarberShop");
                    }
                    break;
            }

            if (result == -1) // Client with such email already exists
            {
                Log.Information("Register failed with email: {Email}, password: {Password}, name: {FirstName} {LastName}", registerViewModel.Email, registerViewModel.Password, registerViewModel.FirstName, registerViewModel.LastName);
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
