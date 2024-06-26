using Microsoft.AspNetCore.Mvc;
using CrochetWebshop.Enums;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Interfaces.iService;
using CrochetWebshop.Models;

namespace CrochetWebshop.Services
{
    public class UserService : iUserService
    {
        public iCustomerService _customerService;
        public iUserRepository _userRepository;

        public UserService(iUserRepository iuserRepository, iCustomerService icustomerService)
        {
            _userRepository = iuserRepository;
            _customerService = icustomerService;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            if (await _userRepository.GetUserByEmail(user.Email) != null)
            {
                Console.WriteLine($"de gevonden user is, email: wachtwoord: ");
                return false;
            }
            else
            {
                user.Password = PasswordHasher.HashPassword(user.Password);
                user.Role = RolesEnum.Customer.ToString();
                await _userRepository.AddUserAsync(user);
                await _customerService.AddCustomerAsync(user);
                return true;
            }
        }

        public async Task<ActionResult<List<User>>> GetAllUsersAsync()
        => await _userRepository.GetAllUsersAsync();

        public async Task<User?> GetUserByEmailAsync(string email)

            => await _userRepository.GetUserByEmail(email);

        public async Task<User?> GetUserByIdAsync(int id)
            => await _userRepository.GetUserById(id);

        public async Task<bool> ValidateUser(string email, string password)
        {
            User? user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            }
            return PasswordHasher.VerifyPassword(password, user.Password);
        }
    }
}