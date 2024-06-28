using CrochetWebshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrochetWebshop.Interfaces.iService
{
    public interface iUserService
    {
        public Task<bool> AddUserAsync(User user);

        public Task<ActionResult<List<User>>> GetAllUsersAsync();

        public Task<User?> GetUserByEmailAsync(string email);

        public Task<User?> GetUserByIdAsync(int id);

        public Task<bool> PromoteToCreator(int userId);

        public Task<bool> ValidateUser(string email, string password);
    }
}