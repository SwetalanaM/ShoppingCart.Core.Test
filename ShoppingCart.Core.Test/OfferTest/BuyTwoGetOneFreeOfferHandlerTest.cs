using Moq;
using ShoppingCart.Core.Domain;
using ShoppingCart.Core.Processor;
using ShoppingCart.Core.Processor.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Test.OfferTest
{
    public class BuyTwoGetOneFreeOfferHandlerTest
    {

        BuyTwoGetOneFreeOfferHandler handler;
        Mock<OfferItem> mockOfferItem;
        OfferItem offerItem;
        Mock<List<BillDiscount>> mockBillDiscount;
        IList<Product> products;
        
        public BuyTwoGetOneFreeOfferHandlerTest()
        {
            //Initialize
            mockOfferItem = new Mock<OfferItem>();
            mockBillDiscount = new Mock<List<BillDiscount>>();
            products = new List<Product>();           
            handler = new BuyTwoGetOneFreeOfferHandler(mockOfferItem.Object, products);
            offerItem = new OfferItem { ItemName = new List<string> { "Apple", "Orange" }, Description = "Mix n Match Buy 2 Get One Free", MinQuantity = 3 };
        }
        [Fact]
        public void NoHandlerTest()
        {

            //Arrange
            handler = null;
            //Assert
            Assert.Null(handler);
        }

        [Fact]
        public void HasNextOfferHandlerFalse()
        {

            //Act 
            var hasNext = handler.HasNext();
            //Assert
            Assert.Equal(false, hasNext);
        }



        [Fact]
        public void HasNextOfferHandlerTrue()
        {
            //Arrange
            BuyTwoGetOneFreeOfferHandler offerHandler = new BuyTwoGetOneFreeOfferHandler(mockOfferItem.Object, products);
            handler.SetNextHandler(offerHandler);
            //Act 
            var hasNext = handler.HasNext();
            //Assert
            Assert.Equal(true, hasNext);
        }

        [Fact]
        public void AddThreeAppleApplyOneOfferGenerateOneDiscount()
        {
            //Arrange       
         
            products.Add(new Product { Id =Guid.NewGuid(), Name = "Apple", Quantity = 3, PriceUnit = 2.0 });
            handler = new BuyTwoGetOneFreeOfferHandler(offerItem, products);
            //Act 
            List<BillDiscount> discounts = handler.ApplyOffer(mockBillDiscount.Object);
            //Assert
            Assert.Equal(1, discounts.Count);
        }
        [Fact]
        public void AddTwoAppleOfferOneOrangeNoOfferGenerateZeroDiscount()
        {

            //Arrange         
            products.Add(new Product { Id =Guid.NewGuid(), Name = "Apple", Quantity = 2, PriceUnit = 2.0 });
            products.Add(new Product { Id =Guid.NewGuid(), Name = "Orange", Quantity = 1, PriceUnit = 1.0 });
            offerItem = new OfferItem { ItemName = new List<string> { "Apple" }, Description = "Mix n Match Buy 2 Get One Free", MinQuantity = 3 };
            handler = new BuyTwoGetOneFreeOfferHandler(offerItem, products);
            //Act 
            List<BillDiscount> discounts = handler.ApplyOffer(mockBillDiscount.Object);
            //Assert
            Assert.Equal(0, discounts.Count);
        }

        [Fact]
        public void AddThreeAppleCalculateDiscountAmount()
        {
            //Arrange
            products.Add(new Product { Id =Guid.NewGuid(), Name = "Apple", Quantity = 3 ,PriceUnit = 1.0 });
            handler = new BuyTwoGetOneFreeOfferHandler(offerItem, products);
            //Act 
            BillDiscount discounts = handler.ApplyOffer(mockBillDiscount.Object).FirstOrDefault();
            //Assert
            Assert.Equal(1.0, discounts.TotalDiscount);
        }

        [Fact]
        public void AddTwoAppleOneOrangeCalculateDiscountAmount()
        {

            //Arrange
            products.Add(new Product { Id =Guid.NewGuid(), Name = "Apple", Quantity = 2, PriceUnit = 2.0 });
            products.Add(new Product { Id =Guid.NewGuid(), Name = "Orange", Quantity = 1, PriceUnit = 1.0 });

            handler = new BuyTwoGetOneFreeOfferHandler(offerItem, products);
            //Act 
            List<BillDiscount> discounts = handler.ApplyOffer(mockBillDiscount.Object);
            //Assert
            Assert.Equal(1.0, discounts[0].TotalDiscount);
        }
    }
}
