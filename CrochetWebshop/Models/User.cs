using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrochetWebshop.DAL;

namespace CrochetWebshop.Models
{
    public class User
    {
        public User()
        {
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
    }
}