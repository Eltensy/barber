using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Serilog;
using System.Net;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer.Services.Identity;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Serilog.Core;


namespace BusinessLogicLayer.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly IAdminService _adminService;
        private readonly IBarberService _barberService;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        // private readonly IEmailSenderService _emailSender;

        public RegisterService(IAdminService adminService,
            IBarberService barberService,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager)
        {
            _adminService = adminService;
            _barberService = barberService;

            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
        }

        public async Task<int> AdminAddBarber(RegistrationDto registrationDto)
        {
            Log.Information("Admin add Barber with email: {Email}", registrationDto.Email);
            UserExtDto userExtDto = new UserExtDto();

            var existingBarber = await _barberService.GetBarberByEmail(registrationDto.Email);
            if (existingBarber != null)
            {
                userExtDto.ErrorMsg = "Barber already exists!";

                return -1;
            }
            else
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, registrationDto.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, registrationDto.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, registrationDto.Password);

                if (result.Succeeded)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        // return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _userManager.AddToRoleAsync(user, "Barber");
                    }

                    //userExtDto = new UserExtDto(newClient);
                    Log.Information("Admin successfully registered the barber with" +
                        "Email: {Email}", registrationDto.Email);

                    return 0;
                }

                return -1;
            }
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
