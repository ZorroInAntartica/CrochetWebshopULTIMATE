using CrochetWebshop.DAL;
using CrochetWebshop.Models;
using CrochetWebshop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private Connection1Context _context;
        private CustomerRepository _customerRepository;
        private DbContextOptions<Connection1Context> _options;

        [Test]
        public async Task AddCustomerAsync_ShouldAddCustomer()
        {
            // Arrange
            var customer = new Customer { Email = "test@example.com" };

            // Act
            await _customerRepository.AddCustomerAsync(customer);
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.Email == "test@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("test@example.com", result.Email);
        }

        [Test]
        public async Task GetCustomerByEmailAsync_ShouldReturnCustomer()
        {
            // Arrange
            var customer = new Customer { Email = "test@example.com" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerRepository.GetCustomerByEmailAsync("test@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("test@example.com", result.Email);
        }

        [Test]
        public async Task GetCustomerByUserIdAsync_ShouldReturnCustomer()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "e", Password = "sywy", Role = "Customer" };
            var customer = new Customer { User = user, Email = "e" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerRepository.GetCustomerByUserIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.User.UserId);
        }

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<Connection1Context>()
                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                        .Options;
            _context = new Connection1Context(_options);
            _customerRepository = new CustomerRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}