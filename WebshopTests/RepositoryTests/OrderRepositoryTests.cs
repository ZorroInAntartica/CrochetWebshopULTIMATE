using CrochetWebshop.DAL;
using CrochetWebshop.Models;
using CrochetWebshop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private Connection1Context _context;
        private DbContextOptions<Connection1Context> _options;
        private OrderRepository _orderRepository;

        [Test]
        public async Task AddOrderAsync_ShouldAddOrder()
        {
            // Arrange
            Order order = new Order
            {
                status = "Pending",
                CreatedDate = "06-06-2005",
                Product = new Product { Productname = "1", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3 },
                Customer = new Customer { Email = "e" },
            };

            // Act
            await _orderRepository.AddOrderAsync(order);
            var result = await _context.Orders.FirstOrDefaultAsync(o => o.status == "Pending");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Pending", result.status);
        }

        [Test]
        public async Task GetAllOrderOfCustomerAsync_ShouldReturnOrders()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Email = "e" };
            var orders = new List<Order>
            {
               new Order { status = "Pending", CreatedDate="06-06-2005", Product = new Product { Productname = "1", Color="c", Description="d", Image="i", PatternCreator="pc", Price=5, TimeToMake=3},  Customer = customer},
               new Order { status = "Pending", CreatedDate="06-06-2005", Product = new Product { Productname = "1", Color="c", Description="d", Image="i", PatternCreator="pc", Price=5, TimeToMake=3}, Customer = customer }
            };
            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderRepository.GetAllOrderOfCustomerAsync(1);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
               new Order { status = "Pending", CreatedDate="06-06-2005", Product = new Product { Productname = "1", Color="c", Description="d", Image="i", PatternCreator="pc", Price=5, TimeToMake=3}, Customer = new Customer { Email = "e" } },
               new Order { status = "Ready", CreatedDate="06-06-2005", Product = new Product { Productname = "1", Color="c", Description="d", Image="i", PatternCreator="pc", Price=5, TimeToMake=3}, Customer = new Customer { Email = "e" } }
            };
            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderRepository.GetAllOrdersAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetOrderById_ShouldReturnOrder()
        {
            // Arrange
            Order order = new Order
            {
                OrderId = 1,
                status = "Pending",
                CreatedDate = "06-06-2005",
                Product = new Product { Productname = "1", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3 },
                Customer = new Customer { Email = "e" },
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderRepository.GetOrderById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.OrderId);
        }

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<Connection1Context>()
                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                        .Options;
            _context = new Connection1Context(_options);
            _orderRepository = new OrderRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task UpdateOrderStatus_ShouldUpdateStatus()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                status = "Pending",
                CreatedDate = "06-06-2005",
                Product = new Product { Productname = "1", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3 },
                Customer = new Customer { Email = "e" }
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _orderRepository.UpdateOrderStatus(1, "Completed");
            var updatedOrder = await _context.Orders.FindAsync(1);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Completed", updatedOrder.status);
        }
    }
}