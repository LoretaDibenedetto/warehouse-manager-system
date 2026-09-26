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
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
            }

            var products = query.ToList();

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
                if (ModelState.IsValid) { 
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
              }
            return View(product);
        }
        

        [HttpPost]
        public IActionResult Delete(int dbID)
        {
            var product = _context.Products.Find(dbID);
            if (product == null) { 
            return NotFound();
            }
            else 
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int dbId)
        {
            var product = _context.Products.Find(dbId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);


        }

        [HttpPost]
        public IActionResult Edit(int dbId, Product product)
        {
            if (ModelState.IsValid)
            {
                var prodotto = _context.Products.Find(dbId);
                if (prodotto == null)
                {
                    return NotFound();
                }
                prodotto.Name = product.Name;
                prodotto.Price = product.Price;
                prodotto.Quantity = product.Quantity;
                prodotto.Category = product.Category;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            product.Id = dbId;
            return View(product);

        }


    }
}
