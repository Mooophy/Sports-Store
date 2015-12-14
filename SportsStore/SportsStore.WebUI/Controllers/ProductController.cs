using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public int PageSize = 4;
        public ProductController(IProductsRepository productRepository)
        {
            repository = productRepository;
        }
        public ViewResult List(int page = 1)
        {
            var products_of_one_page = repository
                .Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(products_of_one_page);
        }
    }
}