using CrochetWebshop.DAL;
using CrochetWebshop.Models;
using CrochetWebshop.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private Connection1Context _context;
        private DbContextOptions<Connection1Context> _options;
        private ProductRepository _productRepository;

        [Test]
        public async Task AddProductAsync_ShouldAddProduct()
        {
            // Arrange
            var product = new Product { Productname = "Product1", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3 };

            // Act
            await _productRepository.AddProductAsync(product);
            var result = await _context.Products.FirstOrDefaultAsync(p => p.Productname == "Product1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Product1", result.Productname);
        }

        [Test]
        public async Task DeleteProductAsync_ShouldDeleteProduct()
        {
            // Arrange
            var product = new Product { Productname = "1", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3 };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            await _productRepository.DeleteProductAsync(1);
            var result = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Productname = "1", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3 },
                new Product {Productname = "1j", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3}
            };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetAllProductsAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetProductById_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product { Productname = "1", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3 };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProductById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ProductId);
        }

        [Test]
        public async Task GetProductByName_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product { Productname = "Product1", Color = "c", Description = "d", Image = "i", PatternCreator = "pc", Price = 5, TimeToMake = 3 };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProductByName("Product1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Product1", result.Productname);
        }

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<Connection1Context>()
                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                        .Options;
            _context = new Connection1Context(_options);
            _productRepository = new ProductRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}