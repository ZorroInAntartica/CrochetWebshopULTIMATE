using CrochetWebshop.Models;

namespace CrochetWebshop.Interfaces.iRepository
{
    public interface iProductRepository
    {
        public Task AddProductAsync(Product product);

        public Task DeleteProductAsync(int productId);

        public Task<List<Product>> GetAllProductsAsync();

        public Task<Product?> GetProductById(int productId);

        public Task<Product?> GetProductByName(string name);
    }
}