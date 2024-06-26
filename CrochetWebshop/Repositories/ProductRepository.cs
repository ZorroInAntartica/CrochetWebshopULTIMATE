using CrochetWebshop.DAL;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Models;
using Microsoft.EntityFrameworkCore;

namespace CrochetWebshop.Repositories
{
    public class ProductRepository : iProductRepository
    {
        private readonly Connection1Context _context;

        public ProductRepository(Connection1Context context)
        { _context = context; }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            Product? product = await GetProductById(productId);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.OrderBy(o => o.Price).ToListAsync();
        }

        public async Task<Product?> GetProductById(int productId)
        {
            await
                foreach (Product product in _context.Products)
            {
                if (product.ProductId == productId)
                {
                    return product;
                }
            }
            return null;
        }

        public async Task<Product?> GetProductByName(string name)
        {
            await
            foreach (Product product in _context.Products)
            {
                if (product.Productname == name)
                {
                    return product;
                }
            }
            return null;
        }
    }
}