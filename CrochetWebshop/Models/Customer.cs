namespace CrochetWebshop.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public User User { get; set; }
    }
}
