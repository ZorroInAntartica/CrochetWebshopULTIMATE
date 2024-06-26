using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrochetWebshop.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private iOrderService _orderService;

        public OrderController(iOrderService iorderService)
        {
            _orderService = iorderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("OrderProduct")]
        public async Task<IActionResult> OrderProduct(int productId)

        {
            string? email = HttpContext.Session.GetString("UserEmail");
            if (email != null)
            {
                bool saved = await _orderService.AddOrderAsync(productId, email);
                if (saved)
                {
                    return RedirectToAction(nameof(OrdersOverview));
                }
                else
                {
                    return RedirectToAction(nameof(OrdersOverview));
                }
            }
            else
            {
                return RedirectToAction(nameof(OrdersOverview));
            }
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> OrdersOverview()
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail is not null)
            {
                IEnumerable<Order> orders = await _orderService.GetAllOrdersByEmailAsync(userEmail);
                return View(orders);
            }
            return RedirectToAction("LogIn", "Authenticate");
        }

        [HttpPost("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            bool succes = await _orderService.UpdateOrderStatus(orderId, status);
            if (succes)
            {
                return RedirectToAction("OrdersOverview", "Creator");
            }
            else
            {
                return RedirectToAction("OrdersOverview", "Creator");
            }
        }
    }
}