using BarberLayered.Models;
using Microsoft.AspNetCore.Mvc;
using BuinessLogicLayer.Services;
using Serilog;

namespace BarberLayered.Controllers
{
    public class BarberShopController : Controller
    {
        private readonly IBarberShopService _barberShopService;
        private BarberShop? _barberShop;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BarberShopController(IBarberShopService barberShopService,
            IHttpContextAccessor httpContextAccessor)
        {
            _barberShopService = barberShopService;
            _httpContextAccessor = httpContextAccessor;
            _barberShop = null;
        }

        public async Task<IActionResult> Index()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var barberShop = await _barberShopService.GetBarberShopFirst();
            if (barberShop == null)
            {
                Log.Error("No info about BarberShop in DataBase");
            }
            else
            {
                _barberShop = new BarberShop()
                {
                    Id = barberShop.Id,
                    Name = barberShop.Name,
                    Address = barberShop.Address,
                    Description = barberShop.Description,
                    Phone = barberShop.Phone,
                    PhoneSecond = barberShop.PhoneSecond,
                    PhotoUri = barberShop.PhotoUri,
                    UserType = session.GetString("UserType"),
                    userId = session.GetInt32("UserId")
                };
            }

            return View(_barberShop);
        }
    }
}