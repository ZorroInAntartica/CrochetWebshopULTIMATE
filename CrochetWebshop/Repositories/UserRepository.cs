using CrochetWebshop.DAL;
using CrochetWebshop.Enums;
using CrochetWebshop.Interfaces.iRepository;
using CrochetWebshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrochetWebshop.Repositories
{
    public class UserRepository : iUserRepository
    {
        private readonly Connection1Context _context;

        public UserRepository(Connection1Context context)
        { _context = context; }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<List<User>>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            await
            foreach (User user in _context.Users)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<User?> GetUserById(int id)
        {
            await
            foreach (User user in _context.Users)
            {
                if (user.UserId == id)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<bool> UpdateRoleAsync(int userId, RolesEnum role)
        {
            User? user = await GetUserById(userId);
            if (user is not null)
            {
                user.Role = role.ToString();
                _context.Attach(user);
                _context.Entry(user).Property("Role").IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}