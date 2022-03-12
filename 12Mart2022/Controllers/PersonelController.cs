using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _12Mart2022.Controllers
{
    public class PersonelController : Controller
    {
        mysqlEntities db = new mysqlEntities();
        // GET: Personel
        public ActionResult Index()
        {
            var model = db.Personel.ToList();
            return View(model);
        }
    }
}