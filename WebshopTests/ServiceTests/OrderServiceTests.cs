using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using CrochetWebshop.Services;
using Moq;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<iCustomerRepository> _mockCustomerRepository;
        private Mock<iOrderRepository> _mockOrderRepository;
        private Mock<iProductRepository> _mockProductRepository;
        private Mock<iUserRepository> _mockUserRepository;
        private iOrderService _orderService;

        [Test]
        public async Task AddOrderAsync_ShouldReturnFalseIfCustomerHasFiveOrders()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1 };
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByEmailAsync(It.IsAny<string>())).ReturnsAsync(customer);
            _mockProductRepository.Setup(repo => repo.GetProductById(It.IsAny<int>())).ReturnsAsync(new Product());
            _mockOrderRepository.Setup(repo => repo.GetAllOrderOfCustomerAsync(It.IsAny<int>())).ReturnsAsync(new List<Order> { new Order(), new Order(), new Order(), new Order(), new Order() });

            // Act
            var result = await _orderService.AddOrderAsync(1, "test@example.com");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddOrderAsync_ShouldReturnFalseIfCustomerNotFound()
        {
            // Arrange
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByEmailAsync(It.IsAny<string>())).ReturnsAsync((Customer)null);

            // Act
            var result = await _orderService.AddOrderAsync(1, "test@example.com");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddOrderAsync_ShouldReturnTrueIfOrderIsAddedSuccessfully()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1 };
            _mockCustomerRepository.Setup(repo => repo.GetCustomerByEmailAsync(It.IsAny<string>())).ReturnsAsync(customer);
            _mockProductRepository.Setup(repo => repo.GetProductById(It.IsAny<int>())).ReturnsAsync(new Product());
            _mockOrderRepository.Setup(repo => repo.GetAllOrderOfCustomerAsync(It.IsAny<int>())).ReturnsAsync(new List<Order>());
            _mockOrderRepository.Setup(repo => repo.AddOrderAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.AddOrderAsync(1, "test@example.com");

            // Assert
            Assert.IsTrue(result);
        }

        [SetUp]
        public void SetUp()
        {
            _mockOrderRepository = new Mock<iOrderRepository>();
            _mockCustomerRepository = new Mock<iCustomerRepository>();
            _mockProductRepository = new Mock<iProductRepository>();
            _mockUserRepository = new Mock<iUserRepository>();
            _orderService = new OrderService(
                _mockOrderRepository.Object,
                _mockCustomerRepository.Object,
                _mockProductRepository.Object,
                _mockUserRepository.Object
            );
        }
    }
}