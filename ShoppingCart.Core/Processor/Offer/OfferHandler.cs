using ShoppingCart.Core.Domain;

namespace ShoppingCart.Core.Processor.Offer
{
    public abstract class OfferHandler
    {
        public OfferHandler nextOfferHandler;        
        public bool HasNext()
        {
            return nextOfferHandler != null;
        }

        public void SetNextHandler(OfferHandler offerHandler)
        {
            nextOfferHandler = offerHandler;
        }
        public abstract List<BillDiscount> ApplyOffer(List<BillDiscount> billDiscount);
    }
}