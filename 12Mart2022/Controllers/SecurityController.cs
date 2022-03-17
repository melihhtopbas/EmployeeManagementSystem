using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _12Mart2022.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        mysqlEntities db = new mysqlEntities();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb!=null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDb.Ad, false);
                ViewBag.Mesaj1 = "Giriş başarılı.. Yönlendiriliyorsunuz..";
                return RedirectToAction("Index","Departman");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya şifre!";
                return View();
            }
            
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}