using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _12Mart2022.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }
    }
}