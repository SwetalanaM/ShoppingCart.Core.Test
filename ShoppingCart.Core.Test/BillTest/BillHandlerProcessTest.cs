using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ShoppingCart.Core.Domain;
using ShoppingCart.Core.Processor;
using ShoppingCart.Core.Processor.Bill;
using ShoppingCart.Core.Processor.Offer;

namespace ShoppingCart.Core.Test.BillTest
{
    public class BillHandlerProcessTest
    {
        IBillHandlerProcess billHandler;
        IList<Product> products;
        IList<BillDiscount> billDiscounts;
        ShoppingBill bill;
        IOfferHandlerProcess offerHandlerProcess;
        OfferItem offerItem;        
        public BillHandlerProcessTest()
        {
            //Initialize
            products = new List<Product>();
            offerItem = new OfferItem { ItemName = new List<string> { "Apple" ,"Orange"}, Description = "Mix n Match Buy 2 Get One Free", MinQuantity = 3 };            
            offerHandlerProcess = new OfferHandlerProcess();
            billHandler = new BillHandlerProcess();            
            bill= new ShoppingBill();
            billDiscounts = new  List<BillDiscount>();

        }
        [Fact]

        public void GenerateZeroBillEmptyCart()
        {   
            //Act
            bill = billHandler.GenerateBill(products, billDiscounts);
            //Assert
            Assert.Equal(0, bill.Total);
        }

        [Fact]

        public void GenerateBillWithoutDiscount()
        {
            //Arrange           
            products.Add(new Product() { Id =Guid.NewGuid(), Name = "Apple", PriceUnit = 0.5, Quantity = 2 });           
            //Act 
            bill = billHandler.GenerateBill(products, billDiscounts);
            //Assert
            Assert.Equal(1, bill.Total);

        }

        [Fact]

        public void GenerateBillSameProductTypeWithDiscount()
        {
            //Arrange           
            products.Add(new Product() { Id =Guid.NewGuid(), Name = "Apple", PriceUnit = 0.5, Quantity = 3 });           
            offerHandlerProcess.AddHandler(new BuyTwoGetOneFreeOfferHandler(offerItem, products));
            billHandler = new BillHandlerProcess();
            billDiscounts =offerHandlerProcess.ApplyOffer();
            //Act 
            bill = billHandler.GenerateBill(products, billDiscounts);
            //Assert
            Assert.Equal(1, bill.Total);

        }

        [Fact]

        public void GenerateBillDifferentProductTypeWithDiscount()
        {
            //Arrange            
            products.Add(new Product() { Id =Guid.NewGuid(), Name = "Apple", PriceUnit = 1.5, Quantity = 3 });
            products.Add(new Product() { Id =Guid.NewGuid(), Name = "Orange", PriceUnit = 0.5, Quantity = 2 });
            offerHandlerProcess.AddHandler(new BuyTwoGetOneFreeOfferHandler(offerItem, products));
            billHandler = new BillHandlerProcess();
            billDiscounts =offerHandlerProcess.ApplyOffer();
            //Act 
            bill = billHandler.GenerateBill(products, billDiscounts);
            //Assert
            Assert.Equal(5, bill.Total); 

        }

        [Fact]

        public void GenerateBillOnlyTwoProductWithDiscount()
        {
            //Arrange            
            products.Add(new Product() { Id = Guid.NewGuid(), Name = "Apple", PriceUnit = 1.5, Quantity = 2 });
            offerHandlerProcess.AddHandler(new BuyTwoGetOneFreeOfferHandler(offerItem, products));
            billHandler = new BillHandlerProcess();
            billDiscounts =offerHandlerProcess.ApplyOffer();
            //Act 
            bill = billHandler.GenerateBill(products, billDiscounts);
            //Assert
            Assert.Equal(3, bill.Total);

        }
    }
}
