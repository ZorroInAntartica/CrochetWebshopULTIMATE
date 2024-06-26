using CrochetWebshop.Interfaces.iService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrochetWebshop.Controllers
{
    public class ProductController : Controller
    {
        private iProductService _productService;

        public ProductController(iProductService iproductService)
        {
            _productService = iproductService;
        }

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            return RedirectToAction("OrdersOverview", "Creator");
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ProductsOverview()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }
    }
}