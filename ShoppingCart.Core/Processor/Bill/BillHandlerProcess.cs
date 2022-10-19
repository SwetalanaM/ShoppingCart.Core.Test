
using ShoppingCart.Core.Domain;
using ShoppingCart.Core.Processor.Offer;

namespace ShoppingCart.Core.Processor.Bill
{
    public class BillHandlerProcess: IBillHandlerProcess
    {       
      

        public ShoppingBill GenerateBill(IList<Product> product, IList<BillDiscount> billDiscounts) 
        {
            ShoppingBill shoppingBill = new ShoppingBill { Total =0};
            if (product.Count >0)
            {
                var totalDiscount = BillDiscount(billDiscounts);                
                shoppingBill.SubTotal = TotalofBaseket(product);
                shoppingBill.Total = shoppingBill.SubTotal - totalDiscount;
            }
            return shoppingBill;

        }

        private Double? TotalofBaseket(IList<Product> products)
        {
            double? totalCost = 0.0;
            if (products != null && products.Count > 0)
            {
                foreach (var item in products)
                {
                    item.TotalPrice = item.Quantity * item.PriceUnit;
                    totalCost += item.TotalPrice;
                }
            }
            return totalCost;
        }
        private Double? BillDiscount(IList<BillDiscount> discounts)
        {           
           return discounts!= null? discounts.Count>0? discounts.Sum(x => x.TotalDiscount):0:0;
        }
    }
}