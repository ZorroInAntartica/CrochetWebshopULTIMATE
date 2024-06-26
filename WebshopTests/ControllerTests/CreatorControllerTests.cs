using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CrochetWebshop.Controllers;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using System.Collections.Generic;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class CreatorControllerTests : IDisposable
    {
        private CreatorController _controller;
        private Mock<iOrderService> _mockOrderService;
        private Mock<iProductService> _mockProductService;

        [Test]
        public void AddProduct_Get_ShouldReturnView()
        {
            // Act
            var result = _controller.AddProduct();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task AddProduct_Post_ShouldRedirectToProductsOverview_WhenProductIsAdded()
        {
            // Arrange
            var product = new Product { Productname = "Product1", Price = 10 };
            _mockProductService.Setup(s => s.AddProductAsync(It.IsAny<Product>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AddProduct(product);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("ProductsOverview", redirectResult.ActionName);
        }

        public void Dispose()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task OrdersOverview_ShouldReturnViewWithOrders()
        {
            // Arrange
            var orders = new List<Order> { new Order { OrderId = 1 }, new Order { OrderId = 2 } };
            _mockOrderService.Setup(s => s.GetAllOrdersAsync()).ReturnsAsync(orders);

            // Act
            var result = await _controller.OrdersOverview();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual(orders, viewResult.Model);
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
            _mockOrderService = new Mock<iOrderService>();
            _mockProductService = new Mock<iProductService>();
            _controller = new CreatorController(_mockOrderService.Object, _mockProductService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}