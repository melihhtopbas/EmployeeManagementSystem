using _12Mart2022.Models.EntityFramework;
using _12Mart2022.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _12Mart2022.Controllers
{
    [Authorize]
    [Authorize(Roles = "A,B")]
    public class DepartmanController : Controller
    {
        // GET: Departman
        private mysqlEntities db = new mysqlEntities();

        
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm",new Departman());
        }
        [HttpPost]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            MesajViewModel model = new MesajViewModel();
            if (departman.Id==0)
            {
                db.Departman.Add(departman);
                model.Mesaj = departman.Ad + " departmanı başarıyla eklendi!";

            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman == null)
                    return HttpNotFound();
                else
                    guncellenecekDepartman.Ad = departman.Ad;
                model.Mesaj = departman.Ad + " başarıyla güncellendi!";


            }

            db.SaveChanges();
            model.Status = true;
            model.LinkText = "Departman listesine dönmek için tıklayın.";
            model.Url = "/Departman";
           return View("_Mesaj",model);
        }
        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model==null)
                return HttpNotFound();
            return View("DepartmanForm",model);  
        }
        public ActionResult Sil(int id)
        {
            var silinecekDepartman = db.Departman.Find(id);
            if (silinecekDepartman == null)
            {
                return HttpNotFound();
            }
            else
                db.Departman.Remove(silinecekDepartman);

            db.SaveChanges();
            return RedirectToAction("Index", "Departman");
        }
    }
}