using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.Models.ViewModels;

namespace Northwind.Controllers
{
    public class APIController : Controller
    {
        private readonly int PageSize = 5;
        // this controller depends on the NorthwindRepository (dependency injection)
        private INorthwindRepository repository;
        public APIController(INorthwindRepository repo) => repository = repo;
        
        [HttpGet, Route("api/product")] // This brings JSON (test in Postman)
        // returns list of all products
        public IEnumerable<Product> Get() => repository.Products.OrderBy(p => p.ProductName);

        [HttpGet, Route("api/product/{id}")]
        // returns specific product
        public Product Get(int id) => repository.Products.FirstOrDefault(p => p.ProductId == id);

        [HttpGet, Route("api/product/discontinued/{discontinued}")]
        // returns all products where discontinued = true/false
        public IEnumerable<Product> GetDiscontinued(bool discontinued) => repository.Products.Where(p => p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product")] 
        // returns all products in a specific category
        public IEnumerable<Product> GetByCategory(int CategoryId) => repository.Products.Where(p => p.CategoryId == CategoryId).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}")]
        // returns all products in a specific category where discontinued = true/false
        public IEnumerable<Product> GetByCategoryDiscontinued(int CategoryId, bool discontinued) => repository.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}/maxprice/{maxprice}")]
        // returns all products in a specific category where discontinued = true/false
        // and where unitprice <= maxprice
        public IEnumerable<Product> GetByCategoryDiscontinuedPrice(int CategoryId, bool discontinued, int maxprice) => repository.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued && p.UnitPrice <= maxprice).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/order/shipped/null")] //?
        // returns list of all orders not shipped
        public IEnumerable<Order> GetOrdersNotShippedYet() => repository.Orders.Where(o => o.ShippedDate == null).OrderByDescending(o => o.RequiredDate);

        [HttpGet, Route("api/order/page{page:int}")]
       // public IEnumerable<Order> GetOrders() => repository.Orders.OrderByDescending(o => o.RequiredDate);
        public ApiListViewModel GetPage(int page = 1) =>
            new ApiListViewModel
            {
                Orders = repository.Orders
                    .Select(e => new ApiViewOrder
                    {
                        OrderID = e.OrderID,
                        ShipName = e.ShipName,
                        OrderDate = Convert.ToDateTime(e.OrderDate),
                        RequiredDate = Convert.ToDateTime(e.RequiredDate),
                        ShippedDate = Convert.ToDateTime(e.ShippedDate)

                    }).OrderByDescending(e => e.RequiredDate)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Orders.Count()
                }
            };

        [HttpGet, Route("api/order/required/{num}")]
        // returns list of all orders ordered by shipped date required in a week ??
        public IEnumerable<Order> GetOrdersRequiredSoon2(int num) => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate <= DateTime.Now.AddDays(num) && o.RequiredDate >= DateTime.Now.Date).OrderByDescending(o => o.RequiredDate);
      

        [HttpGet, Route("api/order/required/today")]
        // returns list of all orders ordered by shipped date required in a week ??
        public IEnumerable<Order> GetOrdersRequiredSoon() => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate == DateTime.Today).OrderByDescending(o => o.RequiredDate);

        [HttpGet, Route("api/order/required/overdue")]
        // returns list of orders overdue (past required date)
        public IEnumerable<Order> GetOrdersOverdue() => repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate < DateTime.Today).OrderByDescending(o => o.RequiredDate);

    }
}
