using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;

namespace CrochetWebshop.Services
{
    public class ProductService : iProductService
    {
        public iProductRepository _productRepository;

        public ProductService(iProductRepository iproductRepository)
        {
            _productRepository = iproductRepository;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            if (await _productRepository.GetProductByName(product.Productname) != null)
            {
                Console.WriteLine($"het product is gevonden: {product.Productname} ");
                return false;
            }
            else
            {
                await _productRepository.AddProductAsync(product);
                return true;
            }
        }

        public async Task<bool> CheckForExistingProductName(string name)
            => await (CheckForExistingProductName(name));

        public async Task DeleteProductAsync(int productId)
            => await _productRepository.DeleteProductAsync(productId);

        public async Task<List<Product>> GetAllProductsAsync()
           => await _productRepository.GetAllProductsAsync();
    }
}