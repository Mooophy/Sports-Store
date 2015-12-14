using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTes1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
                new Product {ProductID = 4, Name = "P4" },
                new Product {ProductID = 5, Name = "P5" },
            }.AsQueryable());
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;

            //Assert
            var prodArray = result.ToArray();
            Assert.AreEqual(2, prodArray.Length);
            Assert.AreEqual("P4", prodArray[0].Name);
            Assert.AreEqual("P5", prodArray[1].Name);
        }
    }
}
