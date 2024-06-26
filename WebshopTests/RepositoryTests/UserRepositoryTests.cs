using CrochetWebshop.DAL;
using CrochetWebshop.Models;
using CrochetWebshop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private Connection1Context _context;
        private DbContextOptions<Connection1Context> _options;
        private UserRepository _userRepository;

        [Test]
        public async Task AddUserAsync_ShouldAddUser()
        {
            // Arrange
            var user = new User { Email = "user@example.com", Password = "password", Role = "Customer" };

            // Act
            await _userRepository.AddUserAsync(user);
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == "user@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("user@example.com", result.Email);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Email = "user1@example.com", Password = "password", Role="Customer" },
                new User { Email = "user2@example.com", Password = "password", Role="Customer" }
            };
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetAllUsersAsync();

            // Assert
            Assert.AreEqual(2, result.Value.Count);
        }

        [Test]
        public async Task GetUserByEmail_ShouldReturnUser()
        {
            // Arrange
            var user = new User { Email = "user@example.com", Password = "password", Role = "Customer" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetUserByEmail("user@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("user@example.com", result.Email);
        }

        [Test]
        public async Task GetUserById_ShouldReturnUser()
        {
            // Arrange
            var user = new User { UserId = 1, Email = "user@example.com", Password = "password", Role = "Customer" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetUserById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
        }

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<Connection1Context>()
                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                        .Options;
            _context = new Connection1Context(_options);
            _userRepository = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}