using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberLayered.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            _logger.LogInformation("Accessed Product Index page.");
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            _logger.LogInformation($"Accessed Product Details page for product with ID {id}.");
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            _logger.LogInformation("Accessed Product Create page.");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                _logger.LogInformation("Created a new product.");
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new product.");
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            _logger.LogInformation($"Accessed Product Edit page for product with ID {id}.");
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                _logger.LogInformation($"Edited product with ID {id}.");
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while editing product with ID {id}.");
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            _logger.LogInformation($"Accessed Product Delete page for product with ID {id}.");
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _logger.LogInformation($"Deleted product with ID {id}.");
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting product with ID {id}.");
                return View();
            }
        }
    }
}
