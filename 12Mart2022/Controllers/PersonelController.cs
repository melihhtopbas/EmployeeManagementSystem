using _12Mart2022.Models.EntityFramework;
using _12Mart2022.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _12Mart2022.Controllers
{

    [Authorize(Roles = "A,B")]
    public class PersonelController : Controller
    {
        mysqlEntities db = new mysqlEntities();
        // GET: Personel
        public ActionResult Index()
        {
            var model = db.Personel.ToList();
            return View(model);
        }

        
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = new Personel()

            };
            return View("PersonelForm", model);
        }
        public ActionResult Kaydet(Personel personel)
        {
            MesajViewModel mesajModel = new MesajViewModel();
            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel = personel
                    
                };
                return View("PersonelForm",model);
            }
            if (personel.Id == 0) //ekleme
            {
                db.Personel.Add(personel);
                mesajModel.Mesaj = personel.Ad + " personeli başarıyla eklnedi!";

            }
            else//Güncelleme
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
                mesajModel.Mesaj = personel.Ad + " personeli başarıyla güncellendi";
            }

            db.SaveChanges();
            mesajModel.Status = true;
            mesajModel.LinkText = "Personel listesine dönmek için tıklayın.";
            mesajModel.Url = "/Personel";
            return View("_Mesaj", mesajModel);

        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = db.Personel.Find(id)
            };
            return View("PersonelForm", model);
        }
        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            if (silinecekPersonel == null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(silinecekPersonel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelleriListele(int id)
            
        {
             var model = db.Personel.Where(x=>x.DepartmanId==id).ToList();
            return PartialView(model);
        }
        //public ActionResult ToplamMaas()
        //{
        //    ViewBag.Maas = db.Personel.Sum(x=>x.Maas);
        //    return PartialView();
        //}
        public int? ToplamMaas()
        {
            return db.Personel.Sum(x => x.Maas);
        }
    }
}