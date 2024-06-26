using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using CrochetWebshop.Controllers;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class ProductControllerTests : IDisposable
    {
        private ProductController _controller;
        private Mock<iProductService> _mockProductService;

        [Test]
        public async Task DeleteProduct_ShouldRedirectToOrdersOverview_WhenProductIsDeleted()
        {
            // Arrange
            var productId = 1;

            _mockProductService.Setup(s => s.DeleteProductAsync(productId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("OrdersOverview", redirectResult.ActionName);
            Assert.AreEqual("Creator", redirectResult.ControllerName);
        }

        public void Dispose()
        {
            _controller?.Dispose();
        }

        [Test]
        public void Index_ShouldReturnView()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task ProductsOverview_ShouldReturnViewWithProducts()
        {
            // Arrange
            var products = new List<Product> { new Product { Productname = "Product1" }, new Product { Productname = "Product2" } };

            _mockProductService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.ProductsOverview();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual(products, viewResult.Model);
        }

        [SetUp]
        public void SetUp()
        {
            _mockProductService = new Mock<iProductService>();
            _controller = new ProductController(_mockProductService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}