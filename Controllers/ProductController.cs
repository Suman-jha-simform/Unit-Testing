using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unit_Testing.Models;

namespace Unit_Testing.Controllers
{
    public class ProductController : ApiController
    {
        private readonly List<Product> products;

        public ProductController()
        {
            products = new List<Product>();
        }

        public ProductController(List<Product> productList)
        {
            products = productList;
        }

        public IHttpActionResult GetAllProducts()
        {
            return Ok(products);
        }

        public async Task<IHttpActionResult> GetAllProductsAsync()
        {
            return await Task.FromResult(GetAllProducts());
        }

        public IHttpActionResult GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public async Task<IHttpActionResult> GetProductByIdAsync(int id)
        {
            return await Task.FromResult(GetProductById(id));
        }
    }
}
