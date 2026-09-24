using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Data;
using WarehouseManager.Models;

namespace WarehouseManager.Controllers
{
    public class MovementsController : Controller
    {
        private readonly AppDbContext _context;

        public MovementsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Create()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        [HttpPost]
        public IActionResult Create(int productId, MovementType type, int quantity)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }

            if (type == MovementType.In)
            {
                product.Quantity += quantity;
            }
            else if (type == MovementType.Out && quantity <= product.Quantity)
            {
                product.Quantity -= quantity;
            }
            else
            {
                return BadRequest("Quantità non disponibile in magazzino.");
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
      }
    }