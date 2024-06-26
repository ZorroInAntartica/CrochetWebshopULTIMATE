using CrochetWebshop.DAL;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Models;

namespace CrochetWebshop.Repositories
{
    public class CustomerRepository : iCustomerRepository
    {
        private readonly Connection1Context _context;

        public CustomerRepository(Connection1Context context)
        { _context = context; }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            await
            foreach (Customer customer in _context.Customers)
            {
                if (customer.Email == email)
                {
                    return customer;
                }
            }
            return null;
        }

        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            await
            foreach (Customer customer in _context.Customers)
            {
                if (customer.User.UserId == userId)
                {
                    return customer;
                }
            }
            return null;
        }
    }
}