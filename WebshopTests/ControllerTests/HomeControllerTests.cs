using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrochetWebshop.Controllers;
using CrochetWebshop.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class HomeControllerTests : IDisposable
    {
        private HomeController _controller;
        private Mock<ILogger<HomeController>> _mockLogger;

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
        public void Privacy_ShouldReturnView()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_mockLogger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}