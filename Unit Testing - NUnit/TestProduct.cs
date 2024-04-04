using System.Web.Http.Results;
using Unit_Testing.Controllers;
using Unit_Testing.Models;

namespace Unit_Testing___NUnit
{
    [TestFixture]
    public class ProductControllerTests
    {
        private List<Product> _testProducts;

        [SetUp]
        public void Setup()
        {
            _testProducts = GetTestProducts();
        }

        [Test]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // arrange
            var controller = new ProductController(_testProducts);

            // act
            var result = controller.GetAllProducts() as OkNegotiatedContentResult<List<Product>>;

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(_testProducts.Count, result.Content.Count);
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // arrange
            var controller = new ProductController(_testProducts);

            // act
            var result = await controller.GetAllProductsAsync() as OkNegotiatedContentResult<List<Product>>;

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(_testProducts.Count, result.Content.Count);
        }

        [Test]
        public void GetProductById_ShouldReturnCorrectProduct()
        {
            // arrange
            var controller = new ProductController(_testProducts);

            // act
            var result = controller.GetProductById(4) as OkNegotiatedContentResult<Product>;

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(_testProducts[3].Id, result.Content.Id);
            Assert.AreEqual(_testProducts[3].Name, result.Content.Name);
        }

        [Test]
        public async Task GetProductByIdAsync_ShouldReturnCorrectProduct()
        {
            // arrange
            var controller = new ProductController(_testProducts);

            // act
            var result = await controller.GetProductByIdAsync(4) as OkNegotiatedContentResult<Product>;

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(_testProducts[3].Id, result.Content.Id);
            Assert.AreEqual(_testProducts[3].Name, result.Content.Name);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void GetProductById_ShouldReturnCorrectProductWithData(int productId)
        {
            // arrange
            var controller = new ProductController(_testProducts);

            // act
            var result = controller.GetProductById(productId) as OkNegotiatedContentResult<Product>;

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(_testProducts[productId - 1].Id, result.Content.Id);
            Assert.AreEqual(_testProducts[productId - 1].Name, result.Content.Name);
        }

        [Test]
        public void GetProductById_ShouldReturnNotFound()
        {
            // arrange
            var controller = new ProductController(_testProducts);

            // act
            var result = controller.GetProductById(999);

            // assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up resources if necessary
            _testProducts = null;
        }

        private List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 50000 },
                new Product { Id = 2, Name = "Basketball", Price = 5000 },
                new Product { Id = 3, Name = "Mobile", Price = 35000 },
                new Product { Id = 4, Name = "Desk", Price = 10000 }
            };
        }
    }
}
