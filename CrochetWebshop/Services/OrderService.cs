using CrochetWebshop.Enums;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;

namespace CrochetWebshop.Services
{
    public class OrderService : iOrderService
    {
        public iCustomerRepository _customerRepository;
        public iOrderRepository _orderRepository;
        public iProductRepository _productRepository;
        public iUserRepository _userRepository;

        public OrderService(iOrderRepository iorderRepository, iCustomerRepository icustomerRepository,
            iProductRepository iproductRepository, iUserRepository userRepository)
        {
            _orderRepository = iorderRepository;
            _customerRepository = icustomerRepository;
            _productRepository = iproductRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddOrderAsync(int productId, string email)
        {
            Customer? customer = await _customerRepository.GetCustomerByEmailAsync(email);
            if (customer == null) { return false; }
            else
            {
                List<Order> orders = await _orderRepository.GetAllOrderOfCustomerAsync(customer.CustomerId);
                if (orders.Count < 5)
                {
                    Order order = new Order();
                    order.CreatedDate = DateTime.Now.ToShortDateString();

                    Product? product = await _productRepository.GetProductById(productId);
                    if (product == null) { return false; }
                    else
                    {
                        order.Product = product;
                        order.status = StatusEnum.Pending.ToString();
                        order.Customer = customer;
                        await _orderRepository.AddOrderAsync(order);
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine($"You have 5 orders already.");
                    return false;
                }
            }
        }

        public Task<List<Order>> GetAllOrdersAsync()
                => _orderRepository.GetAllOrdersAsync();

        public async Task<List<Order>> GetAllOrdersByEmailAsync(string email)
        {
            Customer? customer = await _customerRepository.GetCustomerByEmailAsync(email);
            if (customer == null) { return new List<Order>(); }
            else
            {
                // Voorbeeldlijst van Bestellingen, Producten en Klanten
                List<Order> orders = await _orderRepository.GetAllOrderOfCustomerAsync(customer.CustomerId);
                return orders;
            }
        }

        public async Task<List<Order>> GetAllOrdersOfCustomerAsync(int customerId)
                                => await _orderRepository.GetAllOrderOfCustomerAsync(customerId);

        public async Task<string> UpdateOrderStatus(int orderId, string newStatus)
        {
            throw new NotImplementedException();
        }

        Task<bool> iOrderService.UpdateOrderStatus(int orderId, string newStatus)
        {
            throw new NotImplementedException();
        }

        /*
       public async Task<string> UpdateOrderStatus(int orderId, string newStatus)
       {
           Order? order = await _orderRepository.GetOrderById(orderId);
           if (order is not null)
           {
               switch (order.status)
               {
                   case StatusEnum.Accepted.ToString():
                       throw new Exception("status is already accepted, nothing can change that!!!");
               }
           }
       }

       => await _orderRepository.UpdateOrderStatus(orderId, newStatus);
       }*/
    }
}