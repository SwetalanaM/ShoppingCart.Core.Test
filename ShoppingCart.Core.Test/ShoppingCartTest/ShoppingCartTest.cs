using ShoppingCart.Core.Domain;
using ShoppingCart.Core.Processor;
using System.Net.Http.Headers;

namespace ShoppingCart.Core.Test.ShoppingCart
{
    public class ShoppingCartTest
    {
        ShoppingBasket shoppingBasket;
        public ShoppingCartTest()
        {
            //Initialize
            shoppingBasket = new ShoppingBasket();
        }
        [Fact]
        public void CanAddOneProductToShoppingCart()
        {

            Product product = new Product();
            //Act
            shoppingBasket.AddToCart(product);
            //Assert
            Assert.Equal(1, shoppingBasket.Products.Count);
        }
        [Fact]
        public void AddTwoDiffrentProductToShoppingCartMakeTotalCountTwo()
        {
            Product apple = new Product() { Id =Guid.NewGuid(), Name = "Apple" };
            Product orange = new Product() { Id =Guid.NewGuid(), Name = "Orange" };
            //Act
            shoppingBasket.AddToCart(apple);
            shoppingBasket.AddToCart(orange);
            //Assert
            Assert.Equal(2, shoppingBasket.Products.Count);

        }
        [Fact]
        public void AddTwoSameProductToShoppingCartMakeTotalCountOne()
        {
            Guid guid =Guid.NewGuid();
            Product apple1 = new Product() { Id = guid, Name = "Apple" };
            Product apple2 = new Product() { Id = guid, Name = "Apple" };
            //Act
            shoppingBasket.AddToCart(apple1);
            shoppingBasket.AddToCart(apple2);
            //Assert
            Assert.Equal(1, shoppingBasket.Products.Count);

        }
        [Fact]
        public void AddTwoSameProductToShoppingCartMakeTotalQuantityTwo()
        {
            Guid guid =Guid.NewGuid();
            Product apple1 = new Product() { Id = guid, Name = "Apple" };
            Product apple2 = new Product() { Id = guid, Name = "Apple" };
            //Act
            shoppingBasket.AddToCart(apple1);
            shoppingBasket.AddToCart(apple2);
            //Assert
            Assert.Equal(2, shoppingBasket.Products[0].Quantity);

        }
    }
}