using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _12Mart2022.Controllers
{
    [AllowAnonymous] // herkese izin vermek // authentication onayı
    public class SecurityController : Controller
    {
        private masterEntities db = new masterEntities();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb != null)
            {
               
                FormsAuthentication.SetAuthCookie(kullaniciInDb.Ad, false);
                ViewData["mesaj1"] = "Giriş başarılı. Yönlendiriliyorsunuz..";

                
                
               return RedirectToAction("Index","Departman");
            }
            else
            {
                Response.Write("<script language='javascript'>alert(\"Geçersiz kullanıcı adı veya şifre!\")</script>");
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya şifre!";
                
                return View();
            }
            
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Yetki()
        {
            return View();
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
