using CrochetWebshop.Models;

namespace CrochetWebshop.Interfaces.iService
{
    public interface iOrderService
    {
        public Task<bool> AddOrderAsync(int productId, string email);

        public Task<List<Order>> GetAllOrdersAsync();

        public Task<List<Order>> GetAllOrdersByEmailAsync(string email);

        public Task<List<Order>> GetAllOrdersOfCustomerAsync(int customerId);

        public Task<bool> UpdateOrderStatus(int orderId, string newStatus);
    }
}