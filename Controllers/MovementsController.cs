using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Data;
using WarehouseManager.Models;
using System.Linq;
using System;

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

            if(quantity <= 0)
            {
                return BadRequest("la quantita' non puo' essere minore di 0");
            }
            var currentQuantity = product.Quantity;

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
                return BadRequest("Quantità non disponibile in magazzino, numero disponibile: " + currentQuantity);
            }

            var stockMovement = new StockMovement
            {
                ProductId = productId,
                Type = type,
                Quantity = quantity,
                Date = DateTime.Now



            };
            _context.StockMovements.Add(stockMovement);
            _context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }

    }
    }