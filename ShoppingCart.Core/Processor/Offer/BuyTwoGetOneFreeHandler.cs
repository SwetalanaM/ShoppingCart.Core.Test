using ShoppingCart.Core.Domain;

namespace ShoppingCart.Core.Processor.Offer
{
    public class BuyTwoGetOneFreeOfferHandler : OfferHandler
    {
        private OfferItem offerItem;
        private IList<Product> shoppingProduct;
        public BuyTwoGetOneFreeOfferHandler(OfferItem offerItem,IList<Product> shoppingProduct)
        {
            this.offerItem = offerItem;
            this.shoppingProduct = shoppingProduct;
        }

        public override List<BillDiscount> ApplyOffer(List<BillDiscount> billDiscount)
        {
            List<string> productNames = new List<string>();
            if (offerItem != null)
            {
                productNames = offerItem.ItemName;
                var onlyOfferProduct = shoppingProduct.Where(item => productNames.Contains(item.Name)).ToList();
                if (shoppingProduct.Count >= 0 && TotalQuantity(onlyOfferProduct) >= 3)
                {
                    var totalNumberOfDiscount = TotalQuantity(onlyOfferProduct) / offerItem.MinQuantity;
                    double? discountPriceUnit = 0;
                    if (shoppingProduct.Count > 1)
                        //Find lowest Price
                        discountPriceUnit = onlyOfferProduct.OrderBy(x => x.PriceUnit).ToList()[0].PriceUnit;
                    else
                        discountPriceUnit = onlyOfferProduct[0].PriceUnit;


                    billDiscount.Add(new BillDiscount { DiscountName = offerItem.Description, TotalDiscount = totalNumberOfDiscount * discountPriceUnit });
                }
            }
            if (HasNext())
                nextOfferHandler.ApplyOffer(billDiscount);
            return billDiscount;
        }

        int TotalQuantity(IList<Product> products)
        {
            return products != null ?products.Count >0? products.Sum(x=>x.Quantity):0:0;
        }


    }
}