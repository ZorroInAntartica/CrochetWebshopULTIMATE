using NUnit.Framework;
using Moq;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using CrochetWebshop.Services;
using System.Threading.Tasks;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private iCustomerService _customerService;
        private Mock<iCustomerRepository> _mockCustomerRepository;

        [Test]
        public async Task AddCustomerAsync_ShouldCallAddCustomerAsyncOnRepository()
        {
            // Arrange
            var user = new User { Email = "test@example.com" };

            // Act
            await _customerService.AddCustomerAsync(user);

            // Assert
            _mockCustomerRepository.Verify(repo => repo.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [SetUp]
        public void SetUp()
        {
            _mockCustomerRepository = new Mock<iCustomerRepository>();
            _customerService = new CustomerService(_mockCustomerRepository.Object);
        }
    }
}