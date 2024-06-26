using CrochetWebshop.Models;

namespace CrochetWebshop.Interfaces.iRepository
{
    public interface iCustomerRepository
    {
        public Task AddCustomerAsync(Customer customer);

        public Task<Customer?> GetCustomerByEmailAsync(string email);

        public Task<Customer?> GetCustomerByUserIdAsync(int userId);
    }
}