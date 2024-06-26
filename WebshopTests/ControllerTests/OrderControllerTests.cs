using CrochetWebshop.Controllers;
using CrochetWebshop.Interfaces.iService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class OrderControllerTests : IDisposable
    {
        private OrderController _controller;
        private Mock<iOrderService> _mockOrderService;

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

        [SetUp]
        public void SetUp()
        {
            _mockOrderService = new Mock<iOrderService>();
            _controller = new OrderController(_mockOrderService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public async Task UpdateOrderStatus_ShouldRedirectToOrdersOverview_WhenStatusIsUpdated()
        {
            // Arrange
            var orderId = 1;
            var status = "Shipped";

            _mockOrderService.Setup(s => s.UpdateOrderStatus(orderId, status)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateOrderStatus(orderId, status);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("OrdersOverview", redirectResult.ActionName);
            Assert.AreEqual("Creator", redirectResult.ControllerName);
        }
    }
}