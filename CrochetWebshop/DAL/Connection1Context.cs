using CrochetWebshop.Models;
using Microsoft.EntityFrameworkCore;

namespace CrochetWebshop.DAL
{
    public class Connection1Context : DbContext
    {
        public Connection1Context(DbContextOptions<Connection1Context> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}