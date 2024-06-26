using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using CrochetWebshop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<iCustomerService> _mockCustomerService;
        private Mock<iUserRepository> _mockUserRepository;
        private iUserService _userService;

        [Test]
        public async Task AddUserAsync_ShouldReturnFalseIfUserExists()
        {
            // Arrange
            var user = new User { Email = "test@example.com" };
            _mockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userService.AddUserAsync(user);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddUserAsync_ShouldReturnTrueIfUserIsAdded()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Password = "password" };
            _mockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User)null);
            _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _mockCustomerService.Setup(service => service.AddCustomerAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _userService.AddUserAsync(user);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<User> { new User(), new User() };
            _mockUserRepository.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.AreEqual(users, result.Value);
        }

        [Test]
        public async Task GetUserByEmailAsync_ShouldReturnUser()
        {
            // Arrange
            var user = new User { Email = "test@example.com" };
            _mockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByEmailAsync("test@example.com");

            // Assert
            Assert.AreEqual(user, result);
        }

        [Test]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            // Arrange
            var user = new User { UserId = 1 };
            _mockUserRepository.Setup(repo => repo.GetUserById(It.IsAny<int>())).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(1);

            // Assert
            Assert.AreEqual(user, result);
        }

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<iUserRepository>();
            _mockCustomerService = new Mock<iCustomerService>();
            _userService = new UserService(_mockUserRepository.Object, _mockCustomerService.Object);
        }

        [Test]
        public async Task ValidateUser_ShouldReturnFalseIfUserNotFound()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.ValidateUser("test@example.com", "password");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ValidateUser_ShouldReturnTrueIfPasswordIsValid()
        {
            // Arrange
            var password = "password";
            var hashedPassword = PasswordHasher.HashPassword(password);
            var user = new User { Email = "test@example.com", Password = hashedPassword };
            _mockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userService.ValidateUser("test@example.com", password);

            // Assert
            Assert.IsTrue(result);
        }
    }
}