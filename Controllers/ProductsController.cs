using Microsoft.AspNetCore.Mvc;

using WarehouseManager.Data;
using WarehouseManager.Models;

namespace WarehouseManager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(string searchString)
        {
            var products = _context.Products.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products
                    .Where(p => p.Name.ToLower().Contains(searchString.ToLower()))
                    .ToList();
            }

            return View(products);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}
