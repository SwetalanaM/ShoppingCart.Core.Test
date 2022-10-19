using ShoppingCart.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Processor
{
    public interface IShoppingBasket
    {
        void AddToCart(Product product);
        void RemoveFromCart(Product product);
        public IList<Product> Products { get; }
    }
}
