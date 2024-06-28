using Microsoft.AspNetCore.Mvc;
using CrochetWebshop.Models;
using CrochetWebshop.Enums;

namespace CrochetWebshop.Interfaces.iRepository
{
    public interface iUserRepository
    {
        public Task AddUserAsync(User user);

        public Task<ActionResult<List<User>>> GetAllUsersAsync();

        public Task<User?> GetUserByEmail(string email);

        public Task<User?> GetUserById(int id);

        public Task<bool> UpdateRoleAsync(int userId, RolesEnum role);
    }
}