using Microsoft.AspNetCore.Mvc;
using BarberLayered.Models;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Services.Implementations;

namespace BarberLayered.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IBarberShopService _barberShopService;
        private Admin? _admin;
        private BarberShop? _barberShop;

        public AdminHomeController(IAdminService adminService, IBarberShopService barberShopService)
        {
            _adminService = adminService;
            _barberShopService = barberShopService;
            _admin = null;
            _barberShop = null;
        }
        public async Task<IActionResult> Index(int adminId)
        {
            var barberShop = await _barberShopService.GetBarberShopFirst();
            
            if(barberShop != null)
                _barberShop = new BarberShop(barberShop);

            var admin = await _adminService.GetAdminById(adminId);
            if (admin != null)
                _admin = new Admin(admin);


            ViewBag.Admin = _admin;

            return View(_barberShop);
        }

    }
}