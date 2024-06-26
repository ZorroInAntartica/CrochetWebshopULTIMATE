using NUnit.Framework;
using Moq;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using CrochetWebshop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<iProductRepository> _mockProductRepository;
        private iProductService _productService;

        [Test]
        public async Task AddProductAsync_ShouldReturnFalseIfProductExists()
        {
            // Arrange
            var product = new Product { Productname = "TestProduct" };
            _mockProductRepository.Setup(repo => repo.GetProductByName(It.IsAny<string>())).ReturnsAsync(product);

            // Act
            var result = await _productService.AddProductAsync(product);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddProductAsync_ShouldReturnTrueIfProductIsAdded()
        {
            // Arrange
            var product = new Product { Productname = "TestProduct" };
            _mockProductRepository.Setup(repo => repo.GetProductByName(It.IsAny<string>())).ReturnsAsync((Product)null);
            _mockProductRepository.Setup(repo => repo.AddProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _productService.AddProductAsync(product);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteProductAsync_ShouldCallDeleteProductAsyncOnRepository()
        {
            // Arrange
            int productId = 1;

            // Act
            await _productService.DeleteProductAsync(productId);

            // Assert
            _mockProductRepository.Verify(repo => repo.DeleteProductAsync(productId), Times.Once);
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            var products = new List<Product> { new Product(), new Product() };
            _mockProductRepository.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.AreEqual(products, result);
        }

        [SetUp]
        public void SetUp()
        {
            _mockProductRepository = new Mock<iProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }
    }
}