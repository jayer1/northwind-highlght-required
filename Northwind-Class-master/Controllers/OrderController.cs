using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.Models.ViewModels;

namespace Northwind.Controllers
{
    public class OrderController : Controller
    {
        private INorthwindRepository repository;
        //public int PageSize = 4;
        public OrderController(INorthwindRepository repo) => repository = repo;

        [Authorize(Roles = "Employee")]
        //public int Index(int page = 1){
        //    var od = new OrderListViewModel
        //    {
        //        Orders = repository.Orders
        //            .OrderByDescending(o => o.RequiredDate)
        //            .Skip((page - 1) * PageSize)
        //            .Take(PageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = PageSize,
        //            TotalItems = repository.Orders.Count()
        //        }
        //    };
        //    return od.Orders.Count();
        //}
        public ViewResult Index(int page = 1) => View(new OrderListViewModel
        {
        //    Orders = repository.Orders
        //            .OrderByDescending(o => o.RequiredDate)
        //            .Skip((page - 1) * PageSize)
        //            .Take(PageSize),
        //    PagingInfo = new PagingInfo
        //    {
        //        CurrentPage = page,
        //        ItemsPerPage = PageSize,
        //        TotalItems = repository.Orders.Count()
        //    }
        });

        //public ActionResult Index()
        //{
        //    return View(repository.Orders.Where(o => o.RequiredDate < DateTime.Now).OrderBy(o => o.RequiredDate).Skip(10).Take(5));
        //}
        /*
        public ActionResult Index(string searchShipName)
        {

             return View(repository.Orders.Where(o => o.ShipName.StartsWith(searchShipName) || searchShipName == "Vins et alcools Chevalier").ToList());

        }*/

        //[Authorize(Roles = "Employee")]
        //public IActionResult Index(int id)
        //{
        //    ViewBag.id = id;
        //    return View();
        //}
        /*[Authorize(Roles = "Employee")]
        public ActionResult Index()
        {
            return View(repository.Orders.OrderBy(o => o.RequiredDate));
            //return View(repository.Orders.Where(o => o.ShippedDate == null && o.RequiredDate < DateTime.Now).OrderBy(o => o.RequiredDate));
        }
        */
        public IActionResult OrderDetail(int id) => View(repository.OrderDetails.FirstOrDefault(od => od.OrderID == id));

        //public ActionResult Index() => View(repository.Orders.Where(o => o.ShippedDate != null && o.RequiredDate < DateTime.Now).OrderBy(o => o.RequiredDate).Skip(5).Take(5));
        
    }
}

