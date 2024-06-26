using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using CrochetWebshop.Controllers;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using System.Collections.Generic;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class AuthenticateControllerTests : IDisposable
    {
        private AuthenticateController _controller;
        private Mock<iUserService> _mockUserService;

        public void Dispose()
        {
            _controller?.Dispose();
        }

        [Test]
        public void LogIn_Get_ShouldReturnView()
        {
            // Act
            var result = _controller.LogIn();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task LogIn_Post_ShouldReturnView_WhenModelIsInvalid()
        {
            // Arrange
            var user = new User { Email = "", Password = "" };

            // Act
            var result = await _controller.LogIn(user);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Register_Get_ShouldReturnView()
        {
            // Act
            var result = _controller.Register();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Register_Post_ShouldRedirectToLogin_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Password = "password" };

            _mockUserService.Setup(s => s.AddUserAsync(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Register(user);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Login", redirectResult.ActionName);
            Assert.AreEqual("Authenticate", redirectResult.ControllerName);
        }

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<iUserService>();
            _controller = new AuthenticateController(_mockUserService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}