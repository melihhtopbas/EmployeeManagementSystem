using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teleric.Models.EntityFramework;




namespace Teleric.Controllers
{
    public class DropDownListController : Controller
    {

        
        // GET: DropDownList
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CascadingDropDownList()
        {
            return View();
        }

        public JsonResult Cascading_Get_Categories()
        {
            var northwind = new NorthwindEntities();

            return Json(northwind.Categories.Select(c => new { CategoryId = c.CategoryID, CategoryName = c.CategoryName }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cascading_Get_Products(int? categories)
        {
            var northwind = new NorthwindEntities();
            var products = northwind.Products.AsQueryable();

            if (categories != null)
            {
                products = products.Where(p => p.CategoryID == categories);
            }

            return Json(products.Select(p => new { ProductID = p.ProductID, ProductName = p.ProductName }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cascading_Get_Orders(int? products)
        {
            var northwind = new NorthwindEntities();
            var orders = northwind.Order_Details.AsQueryable();

            if (products != null)
            {
                orders = orders.Where(o => o.ProductID == products);
            }

            return Json(orders.Select(o => new { OrderID = o.OrderID, ShipCity = o.Orders.ShipCity }), JsonRequestBehavior.AllowGet);
        }
    }
}