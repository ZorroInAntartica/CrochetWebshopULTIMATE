using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrochetWebshop.Controllers
{
    [Authorize(Roles = "Creator")]
    public class CreatorController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private iOrderService _orderService;
        private iProductService _productService;

        public CreatorController(iOrderService iOrderService, iProductService iproductService)
        {
            _orderService = iOrderService;
            _productService = iproductService;
        }

        [HttpGet("AddProduct")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([Bind("Productname,Description,PatternCreator,Price,TimeToMake,Image,Color")] Product product)
        {
            if (await _productService.AddProductAsync(product) == true)
            {
                return RedirectToAction(nameof(ProductsOverview));
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> OrdersOverview()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> ProductsOverview()
        {
            var orders = await _productService.GetAllProductsAsync();
            return View(orders);
        }
    }
}