using CrochetWebshop.DAL;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Models;
using Microsoft.EntityFrameworkCore;

namespace CrochetWebshop.Repositories
{
    public class OrderRepository : iOrderRepository
    {
        private readonly Connection1Context _context;

        public OrderRepository(Connection1Context context)
        { _context = context; }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrderOfCustomerAsync(int customerId)
        {
            List<Order> orders = await _context.Orders.Include(c => c.Customer).Include(p => p.Product).ToListAsync();
            orders = orders.Select(o => o).Where(o => o.Customer.CustomerId == customerId).ToList();
            return orders;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(c => c.Customer).Include(p => p.Product).OrderBy(o => o.status)
              .ThenBy(o => o.Customer.CustomerId).ToListAsync();
        }

        public async Task<List<Order>> getAllOrdersWithStatus(string status)
        {
            return await _context.Orders.Where(o => o.status == status).OrderBy(o => o.CreatedDate).ToListAsync();
        }

        public async Task<Order?> GetOrderById(int orderId)
            => await _context.Orders.FindAsync(orderId);

        public async Task<bool> UpdateOrderStatus(int orderId, string newStatus)
        {
            Order? order = await GetOrderById(orderId);
            if (order is not null)
            {
                order.status = newStatus;
                _context.Attach(order);
                _context.Entry(order).Property("status").IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}