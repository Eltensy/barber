using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class BarberController : Controller
    {
        private readonly DataAccessLayer.Data.DataContext _dataContext;
        public BarberController(DataAccessLayer.Data.DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}