namespace ShoppingCart.Core.Domain
{
    public class OfferItem
    {
        public List<string> ItemName { get; set; }
        public string Description { get; set; }
        public int?  MinQuantity { get; set; }
        public decimal? OfferPrice { get; set; }
       
    }
}