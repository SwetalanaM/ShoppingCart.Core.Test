using System.Collections.ObjectModel;
using ShoppingCart.Core.Domain;

namespace ShoppingCart.Core.Processor
{
    public class ShoppingBasket : IShoppingBasket
    {
        IList<Product> _Products = new List<Product>();     

        public IList<Product> Products
        {
            get
            {
                return  _Products;
            }
        }
        public void AddToCart(Product product)
        {
            var exitingProduct = _Products.FirstOrDefault(x => x.Name == product.Name);
            if (exitingProduct != null)
            {
                exitingProduct.Quantity++;
            }
            else
                _Products.Add(product);
        }
        public void RemoveFromCart(Product product)
        {
            var exitingProduct = _Products.FirstOrDefault(x => x.Name == product.Name);
            if (exitingProduct != null)
            {
                _Products.Remove(product);
            }
        }    
    }
}