namespace ShoppingCart.Core.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? TotalPrice { get; set; }
        public int Quantity { get; set; }
        public double? PriceUnit { get; set; }
        public Product()
        {
            Quantity = 1;
        }

    }
}