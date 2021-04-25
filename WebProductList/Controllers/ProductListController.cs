using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProductList.Controllers.Shared;
using WebProductList.Models;

namespace WebProductList.Controllers {
    [Authorize]
    public class ProductListController : Controller {
        private readonly ProductDbContext _context;

        public ProductListController(ProductDbContext context) {
            _context = context;
        }

        public ActionResult<IEnumerable<BriefProduct>> Index(string name, double? minPrice, double? maxPrice) {
            ProductList productList = new ProductList(_context);
            return View(productList.GetProducts(name, minPrice, maxPrice));
        }

        public IActionResult Create() {
            return View();
        }

        // Создание продукта
        [HttpPost]
        public async Task<IActionResult> Create(Product product) {
            if (ModelState.IsValid) {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Route("[controller]/edit/{id}")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null) {
                return NotFound();
            }
            return View(product);
        }

        // Редактирование продукта
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product) {
            if (ModelState.IsValid) {
                try {
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!ProductExists(product.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }

        private bool ProductExists(int id) {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
