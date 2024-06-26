using CrochetWebshop.Models;

namespace CrochetWebshop.Interfaces.iService
{
    public interface iCustomerService
    {
        public Task AddCustomerAsync(User user);
    }
}