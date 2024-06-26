using CrochetWebshop.Models;

namespace CrochetWebshop.Interfaces.iService
{
    public interface iProductService
    {
        public Task<bool> AddProductAsync(Product product);

        public Task<bool> CheckForExistingProductName(string name);

        public Task DeleteProductAsync(int productId);

        public Task<List<Product>> GetAllProductsAsync();
    }
}