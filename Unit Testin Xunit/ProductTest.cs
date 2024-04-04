using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Unit_Testing.Controllers;
using Unit_Testing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Unit_Testing.Tests
{
    public class TestProduct
    {
        private List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>();
            testProducts.Add(new Product { Id = 1, Name = "Laptop", Price = 50000 });
            testProducts.Add(new Product { Id = 2, Name = "Basketball", Price = 5000 });
            testProducts.Add(new Product { Id = 3, Name = "Mobile", Price = 35000 });
            testProducts.Add(new Product { Id = 4, Name = "Desk", Price = 10000 });

            return testProducts;
        }

        [Fact]
        public void GetProducts_ShouldReturnAllProducts()
        {
            // arrange
            var testProducts = GetTestProducts();
            var controllerMethod = new ProductController(testProducts);

            // act
            var result = controllerMethod.GetAllProducts() as List<Product>;

            // assert
            Assert.NotNull(result);
            Assert.Equal(testProducts.Count, result.Count);
            // Additional testing scenarios
            Assert.All(result, item => Assert.NotNull(item));
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnAllProducts()
        {
            // arrange
            var testProducts = GetTestProducts();
            var controllerMethod = new ProductController(testProducts);

            // act
            var result = await controllerMethod.GetAllProductsAsync() as List<Product>;

            // assert
            Assert.NotNull(result);
            Assert.Equal(testProducts.Count, result.Count);
            // Additional testing scenarios
            Assert.All(result, item => Assert.NotNull(item));
        }

        [Fact]
        public void GetProduct_ShouldReturnCorrectProduct()
        {
            // arrange
            var testProducts = GetTestProducts();
            var controller = new ProductController(testProducts);

            // act
            var result = controller.GetProductById(4) as OkObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result.Value);
            Assert.Equal(testProducts[3].Name, (result.Value as Product).Name);
            // Additional testing scenarios
            Assert.Equal(4, (result.Value as Product).Id);
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnCorrectProduct()
        {
            // arrange
            var testProducts = GetTestProducts();
            var controller = new ProductController(testProducts);

            // act
            var result = await controller.GetProductByIdAsync(4) as OkObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result.Value);
            Assert.Equal(testProducts[3].Name, (result.Value as Product).Name);
            Assert.Equal(4, (result.Value as Product).Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void GetProduct_ShouldReturnCorrectProductWithData(int productId)
        {
            // arrange
            var testProducts = GetTestProducts();
            var controller = new ProductController(testProducts);

            // act
            var result = controller.GetProductById(productId) as OkObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result.Value);
            Assert.Equal(testProducts[productId - 1].Id, (result.Value as Product).Id);
            Assert.Equal(testProducts[productId - 1].Name, (result.Value as Product).Name);
            // Additional testing scenarios
            Assert.NotNull((result.Value as Product));
        }

        [Fact]
        public void GetProduct_ShouldNotFindProduct()
        {
            // arrange
            var controller = new ProductController(GetTestProducts());

            // act
            var result = controller.GetProductById(999);

            // assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
