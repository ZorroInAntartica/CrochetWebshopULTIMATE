namespace CrochetWebshop.Models
{
    public class Order
    {
        public string CreatedDate { get; set; }
        public Customer Customer { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public string status { get; set; }
    }
}