using ShoppingCart.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Processor.Offer
{
    public interface IOfferHandlerProcess
    {
        void AddHandler(OfferHandler offerHandler);
        List<BillDiscount> ApplyOffer();
    }
}
