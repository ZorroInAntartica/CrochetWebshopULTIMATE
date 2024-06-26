using Microsoft.AspNetCore.Mvc;
using CrochetWebshop.Models;

namespace CrochetWebshop.Interfaces.iRepository
{
    public interface iOrderRepository
    {
        public Task AddOrderAsync(Order order);

        public Task<List<Order>> GetAllOrderOfCustomerAsync(int customerId);

        public Task<List<Order>> GetAllOrdersAsync();

        public Task<List<Order>> getAllOrdersWithStatus(string status);

        public Task<Order?> GetOrderById(int orderId);

        public Task<bool> UpdateOrderStatus(int orderId, string newStatus);
    }
}