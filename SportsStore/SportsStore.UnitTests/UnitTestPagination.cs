using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System.Web.Mvc;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTestPagination
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange 
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
                new Product {ProductID = 4, Name = "P4" },
                new Product {ProductID = 5, Name = "P5" },
            }.AsQueryable());

            // create a controller and make the page size 3 items
            var controller = new ProductController(mock.Object) { PageSize = 3 };

            // Action 
            var result = (ProductsListViewModel)controller.List(2).Model;

            // Assert
            var prodArray = result.Products.ToArray();
            Assert.AreEqual(2, prodArray.Length);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper helper = null;
            var pagingInfo = new PagingInfo { CurrentPage = 2, TotalItems = 28, ItemsPerPage = 10 };
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            MvcHtmlString result = helper.PageLinks(pagingInfo, pageUrlDelegate);
            var expect = @"<a href=""Page1"">1</a>" + @"<a class=""selected"" href=""Page2"">2</a>" + @"<a href=""Page3"">3</a>";
            Assert.AreEqual(expect, result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock
                .Setup(m => m.Products)
                .Returns
                (
                    new Product[]
                    {
                        new Product {ProductID = 1, Name = "P1"},
                        new Product {ProductID = 2, Name = "P2"},
                        new Product {ProductID = 3, Name = "P3"},
                        new Product {ProductID = 4, Name = "P4"},
                        new Product {ProductID = 5, Name = "P5"}
                    }
                    .AsQueryable()
                );
            var controller = new ProductController(mock.Object) { PageSize = 3 };
            var result = (ProductsListViewModel)controller.List(2).Model;

            //Assert
            Assert.AreEqual(2, result.PagingInfo.CurrentPage);
            Assert.AreEqual(3, result.PagingInfo.ItemsPerPage);
            Assert.AreEqual(5, result.PagingInfo.TotalItems);
            Assert.AreEqual(2, result.PagingInfo.TotalPages);
        }
    }
}
