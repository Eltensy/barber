using BarberLayered.Models;
using Microsoft.AspNetCore.Mvc;
using BuinessLogicLayer.Services;

namespace BarberLayered.Controllers
{
    public class BarberShopController : Controller
    {
        private readonly IBarberShopService _barberShopService;
        private readonly BarberShop _barberShop;

        public BarberShopController(IBarberShopService barberShopService)
        {
            _barberShopService = barberShopService;
        }

        //private readonly BarberShop _barberShop = new BarberShop
        //{
        //    Id = 1,
        //    Name = "Example Barber Shop",
        //    Address = "123 Example St",
        //    Phone = "+1234567890",
        //    Description = "Наш барбершоп - це сучасний заклад, де кожен клієнт отримує персоналізований сервіс від професійних барберів. Ми знаходимося в центрі міста і пропонуємо широкий спектр послуг, від стрижок і гоління до догляду за бородою та вусами. Наш колектив складається з досвідчених майстрів, які завжди готові задовольнити ваші потреби в стилі та догляді."
        //};

        public IActionResult Index()
        {
            var barberShop = _barberShopService.GetBarberShopFirst();
            if (barberShop == null)
            {
                throw new Exception("No BarberShop info in DB");
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
                }
            }

            return View(_barberShop);
        }
    }
}