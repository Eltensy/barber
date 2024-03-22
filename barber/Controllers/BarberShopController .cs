using barber.Models;
using Microsoft.AspNetCore.Mvc;

namespace barber.Controllers
{
    public class BarberShopController : Controller
    {
        private readonly BarberShop _barberShop = new BarberShop
        {
            Id = 1,
            Name = "Example Barber Shop",
            Address = "123 Example St",
            Phone = "+1234567890",
            Description = "Наш барбершоп - це сучасний заклад, де кожен клієнт отримує персоналізований сервіс від професійних барберів. Ми знаходимося в центрі міста і пропонуємо широкий спектр послуг, від стрижок і гоління до догляду за бородою та вусами. Наш колектив складається з досвідчених майстрів, які завжди готові задовольнити ваші потреби в стилі та догляді."
        };

        public IActionResult Index()
        {
            return View(_barberShop);
        }
    }
}
