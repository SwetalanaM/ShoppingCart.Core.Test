
namespace ShoppingCart.Core.Domain
{
    public class ShoppingBill
    {
        public double? Total { get; internal set; }

        public double? SubTotal { get; set; }

        public string Currency { get; set; }

        public List<BillDiscount> DiscountAmount { get; set; }
    }
}