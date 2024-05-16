using Microsoft.AspNetCore.Mvc;
using BarberLayered.Models;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using Serilog;
using BarberShop = BarberLayered.Models.BarberShop;
using Admin = BarberLayered.Models.Admin;
using Microsoft.AspNetCore.Authorization;

namespace BarberLayered.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        private readonly IBarberShopService _barberShopService;
        private readonly IAdminService _adminService;
        private Admin? _admin;
        private BarberShop? _barberShop;

        public AdminHomeController(IBarberShopService barberShopService, IAdminService adminService)
        {
            _barberShopService = barberShopService;
            _adminService = adminService;
            _admin = null;
            _barberShop = null;
        }
        public async Task<IActionResult> Index(Admin admin)
        {
            var barberShop = await _barberShopService.GetBarberShopFirst();
            
            if(barberShop != null)
                _barberShop = new BarberShop(barberShop);

            _admin = admin;

            ViewBag.Admin = _admin;

            return View(_barberShop);
        }

        public ActionResult AddBarber()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBarber(BarberLayered.Models.Barber barber)
        {
            //if (ModelState.IsValid)
            //{
            //    BarberDto barberDto = new BarberDto()
            //    {
            //        Name = barber.Name,
            //        Surname = barber.Surname,
            //        Phone = barber.Phone,
            //        Email = barber.Email,
            //        Description = barber.Description,
            //    };

            //    try
            //    {
            //        // Add the new barber

            //        TempData["SuccessMessage"] = "Barber has been successfully added!";
            //    }
            //    catch (Exception ex)
            //    {
            //        // Handling errors during data addition
            //        TempData["ErrorMessage"] = "Incorrectly entered data";
            //    }
            //}
            //else
            //{
            //    //TempData["ErrorMessage"] = "Incorrectly entered data";
            //}

            return RedirectToAction("AddBarber");
        }


        public async Task<IActionResult> AdminAccount(int adminId)
        {
            var admin = await _adminService.GetAdminById(adminId);

            if (admin == null)
            {
                Log.Error("No info about Admin with id={Id} was found in the DataBase", adminId);
            }
            else
            {
                _admin = new Admin(admin);
            }

            ViewBag.Admin = _admin;
            return View(_admin);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                AdminDto adminDto = new AdminDto()
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Surname = admin.Surname,
                    Phone = admin.Phone,
                    Email = admin.Email,
                };

                try
                {
                    // Update the admin
                    await _adminService.UpdateAdmin(adminDto);

                    TempData["SuccessMessage"] = "Your data has been successfully updated!";
                }
                catch (Exception ex)
                {
                    // Handling errors during data update
                    TempData["ErrorMessage"] = "Incorrectly entered data";
                }
            }
            else
            {
                //TempData["ErrorMessage"] = "Incorrectly entered data";
            }

            return RedirectToAction("AdminAccount", new { adminId = admin.Id });
        }


    }
}