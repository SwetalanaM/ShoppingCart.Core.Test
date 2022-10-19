using ShoppingCart.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Processor.Bill
{
    public interface IBillHandlerProcess
    {
        ShoppingBill GenerateBill(IList<Product> product,IList<BillDiscount> billDiscounts);
    }
}
