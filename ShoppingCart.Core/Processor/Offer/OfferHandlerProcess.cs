using ShoppingCart.Core.Domain;

namespace ShoppingCart.Core.Processor.Offer
{
    public class OfferHandlerProcess : IOfferHandlerProcess
    {
        public OfferHandler firstOfferHandler;
        public OfferHandler nextOfferHandler;       
        public void AddHandler(OfferHandler offerHandler)
        {
            if (firstOfferHandler == null)
                firstOfferHandler = offerHandler;
            else
                nextOfferHandler.SetNextHandler(offerHandler);
            nextOfferHandler = offerHandler;
        }

        public List<BillDiscount> ApplyOffer()
        {
            List<BillDiscount> billingDiscount = new List<BillDiscount>();  
            if(firstOfferHandler != null)
            billingDiscount = firstOfferHandler.ApplyOffer(billingDiscount);
            return billingDiscount;
        }
    }
}