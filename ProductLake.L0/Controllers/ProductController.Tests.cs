using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using ProductLake.Controllers;
using ProductLake.DataManagerService;
using ProductLake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace ProductLakeTest.L0.Controllers
{
    internal class ProductControllerTests
    {
        private Mock<IDataStoreServiceFactory> serviceFactoryMock;
        private Mock<IProductDataStore> dataStoreMock;
        private ProductController controller;

        [SetUp]
        public void Startup()
        {
            this.serviceFactoryMock = new Mock<IDataStoreServiceFactory>();
            this.dataStoreMock = new Mock<IProductDataStore>();
            this.controller = new ProductController(this.serviceFactoryMock.Object);
        }

        [Test]
        public void ShouldGetProductListWhenRequestIsAuthorized() 
        {
            // Arrange.
            IEnumerable<Product> products = new List<Product>()
            {
                new Product("Product1"),
                new Product("Product2"),
            };
            
            this.serviceFactoryMock
            .Setup(item => item.GetDataStore(It.IsAny<string>()))
            .Returns(this.dataStoreMock.Object);

            this.dataStoreMock
            .Setup(store => store.RetrieveProductList())
            .Returns(products);
            
            // Act.
            IEnumerable<Product> productResponse = this.controller.GetProducts();

            // Assert.
            this.serviceFactoryMock.Verify(call => call.GetDataStore(It.IsAny<string>()), Times.Once);
            this.dataStoreMock.Verify(call => call.RetrieveProductList(), Times.Once);
            Assert.AreEqual(products, productResponse);
        }

        // ..... other tests..
        // exception is thrown by one of the services.
        // test for GetProductsByColour

    }
}
