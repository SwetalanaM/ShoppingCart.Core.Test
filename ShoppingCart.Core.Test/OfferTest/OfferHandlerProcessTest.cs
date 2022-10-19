using Moq;
using ShoppingCart.Core.Domain;
using ShoppingCart.Core.Processor;
using ShoppingCart.Core.Processor.Offer;

namespace ShoppingCart.Core.Test.OfferTest
{
    public class OfferHandlerProcessTest
    {

        IOfferHandlerProcess processor;        
        IList<Product> products;
        OfferItem offerItem;
        public OfferHandlerProcessTest()
        {
            //Initialize            
            offerItem = new OfferItem { ItemName = new List<string> { "Apple" }, Description = "Mix n Match Buy 2 Get One Free", MinQuantity = 3 };
            processor = new OfferHandlerProcess();
            products = new List<Product>();
        }
        [Fact]

        public void AddHandlerApplyOfferNoOffer()
        {
            //Act
            List<BillDiscount> billDiscounts = processor.ApplyOffer();
            //Assert
            Assert.Equal(0, billDiscounts.Count);
        }
        [Fact]
        public void AddOneHandlerApplyOffer()
        {
            products.Add(new Product { Id =Guid.NewGuid(), Name = "Apple", Quantity = 3, PriceUnit = 2.0 });            
            //Act
            processor.AddHandler(new BuyTwoGetOneFreeOfferHandler(offerItem, products));
            List<BillDiscount> billDiscounts = processor.ApplyOffer();
            //Assert
            Assert.Equal(1, billDiscounts.Count);
        }

        [Fact]
        public void AddThreeHandlerApplyOffer()
        {
            //Arrange           

            products.Add(new Product { Id =Guid.NewGuid(), Name = "Apple", Quantity = 3, PriceUnit = 2.0 });
            //Act
            processor.AddHandler(new BuyTwoGetOneFreeOfferHandler(offerItem, products));
            processor.AddHandler(new BuyTwoGetOneFreeOfferHandler(offerItem, products));
            processor.AddHandler(new BuyTwoGetOneFreeOfferHandler(offerItem, products));
            List<BillDiscount> billDiscounts = processor.ApplyOffer();
            //Assert
            Assert.Equal(3, billDiscounts.Count);
        }
    }
}
