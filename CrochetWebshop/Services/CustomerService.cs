using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;

namespace CrochetWebshop.Services
{
    public class CustomerService : iCustomerService

    {
        public iCustomerRepository _customerRepository;

        public CustomerService(iCustomerRepository icustomerRepository)
        {
            _customerRepository = icustomerRepository;
        }

        public async Task AddCustomerAsync(User user)
        {
            Customer customer = new Customer();
            customer.Email = user.Email;
            customer.User = user;
            await _customerRepository.AddCustomerAsync(customer);
        }
    }
}