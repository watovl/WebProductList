using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProductList.Models;

namespace WebProductList.Controllers.Shared {
    public class ProductList {
        ProductDbContext _context;
        public ProductList(ProductDbContext context) {
            _context = context;
        }

        public IEnumerable<BriefProduct> GetProducts(string name, double? minPrice, double? maxPrice) {
            IEnumerable<BriefProduct> products = _context.Products.ToList();
            if (!string.IsNullOrWhiteSpace(name)) {
                products = products.Where(p => p.Name.Contains(name));
            }
            if (maxPrice != null) {
                products = products.Where(p => p.Price >= (minPrice ?? 0.0) & p.Price <= maxPrice);
            }
            else if (minPrice != null) {
                products = products.Where(p => p.Price >= minPrice);
            }
            return products;
        }

        public async Task<Product> GetProduct(int id) {
            Product product = await _context.Products.FindAsync(id);
            if (product == null) {
                return default(Product);
            }
            return product;
        }
    }
}
