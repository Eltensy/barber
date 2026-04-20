using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BarberLayered.Controllers
{
    public class AddBarber : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        // private readonly IEmailSenderService _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddBarber(ILoginService loginService, IRegisterService registerService,
            IHttpContextAccessor httpContextAccessor)//, IEmailSenderService emailSender)
        {
            _loginService = loginService;
            _registerService = registerService;
            _httpContextAccessor = httpContextAccessor;
            // _emailSender = emailSender;
        }

        // GET: /AdminHome/AddBarber
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AddBarber/AddNewBarber
        [HttpPost]
        public async Task<IActionResult> AddNewBarber(string? name,
            string? surname,
            string? password,
            string? email,
            string? phone)
        {
            RegistrationDto registrationDto = new RegistrationDto()
            {
                //Email = barber.Email,
                //Name = barber.Name,
                //Surname = barber.Surname,
                //Phone = barber.Phone,

                Email = email,
                Name = name,
                Surname = surname,
                Phone = phone,
                Password = password
            };

            var result = await _registerService.AdminAddBarber(registrationDto);
            if (0 != result)
            {
                TempData["ErrorMessage"] = "Error happened(";
                return View("../AdminHome/AddBarber");
            }

            TempData["SuccessMessage"] = "Barber successfully added.";
            return View("../AdminHome/Index");
        }

    }
}
