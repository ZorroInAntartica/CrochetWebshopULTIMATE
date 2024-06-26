namespace CrochetWebshop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Productname { get; set; }
        public string Description { get; set; }
        public string PatternCreator { get; set; }
        public int Price { get; set; }
        public int TimeToMake { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
    }
}
