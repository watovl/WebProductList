using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProductList.Controllers.Shared;
using WebProductList.Models;

namespace WebProductList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        ProductList _productList;
        public ProductController(ProductDbContext context) {
            _productList = new ProductList(context);
        }

        // Вернуть список отфильтрованных продуктов
        [HttpGet]
        public ActionResult<IEnumerable<BriefProduct>> Get(string name, double? minPrice, double? maxPrice) {
            return new ObjectResult(_productList.GetProducts(name, minPrice, maxPrice));
        }

        // Вернуть продукт по id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id) {
            return new ObjectResult(await _productList.GetProduct(id));
        }
    }
}