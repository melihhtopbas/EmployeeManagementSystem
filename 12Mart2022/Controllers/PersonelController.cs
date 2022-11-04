using _12Mart2022.Models.EntityFramework;
using _12Mart2022.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _12Mart2022.Controllers
{

    [Authorize]
    public class PersonelController : Controller
    {
        masterEntities db = new masterEntities();
        // GET: Personel
        public ActionResult Index()
        {
           

            var model = db.Personel.Select(x => new PersonelListVM
            {

                Id = x.Id,
                Resimler = x.Resim.Select(a => a.PathName).ToList(),
                Soyad = x.Soyad,
                Ad = x.Ad,
                Departman = x.Departman,
                DepartmanId = x.DepartmanId,
                EvliMi = x.EvliMi,
                Maas = x.Maas,
                DogumTarihi = x.DogumTarihi,
                Cinsiyet = x.Cinsiyet,
                PersonelGorsel = x.PersonelGorsel,
            }).ToList();



            return View(model);
        }

        [Authorize(Roles = "A")]
        public ActionResult Yeni()
        {
            //Departman p = new Departman();
            //p = db.Departman.Find(id);
            ////List<Personel> list = new List<Personel>();
            //var list = db.Personel.Where(x => x.DepartmanId == id).ToList();
            //var liste = db.Personel.Where(x => x.DepartmanId == id).Select(per => new PersonelUpdateVM
            //{
            //    Id = per.Id,
            //    Resimler = per.Resim.Select(a => a.PathName).ToList(),
            //    Soyad = per.Soyad,
            //    Ad = per.Ad,
            //    Departman = per.Departman,
            //    DepartmanId = per.DepartmanId,
            //    EvliMi = per.EvliMi,
            //    Maas = per.Maas,
            //    DogumTarihi = per.DogumTarihi,
            //    Cinsiyet = per.Cinsiyet,
            //    PersonelGorsel = per.PersonelGorsel,



            //}).ToList();
            var list = db.Departman.Select(x => new DepartmanListVM
            {
                Ad = x.Ad,
                Id = x.Id,
                Personel = x.Personel,
                PersonelSayisi = x.PersonelSayisi,
            }).ToList();
            var model = new PersonelAddViewModel()
            {
                Departmanlar = list,
                Personel = new PersonelAddVM(),




            };
            return View("PersonelForm", model);
        }
        
        public ActionResult Kaydet(PersonelAddVM personel, HttpPostedFileBase[] fileBases)
        {
            Personel p = new Personel()
            {
                 Id = personel.Id,
                 Ad = personel.Ad,
                 Soyad = personel.Soyad,
                 Cinsiyet = personel.Cinsiyet,
                 Departman = personel.Departman,
                 DepartmanId = personel.DepartmanId,
                 DogumTarihi = personel.DogumTarihi,
                 EvliMi = personel.EvliMi,
                 Maas = personel.Maas,
                //Resimler = x.Resim.Select(a => a.PathName).ToList(),
                Resimler = personel.Resim.Select(a=>a.PathName).ToList(),
            };
            var list = db.Departman.Select(x => new DepartmanListVM
            {
                Ad = x.Ad,
                Id = x.Id,
                Personel = x.Personel,
                PersonelSayisi = x.PersonelSayisi,
            }).ToList();

            // List<string> files = new List<string>();
            MesajViewModel mesajModel = new MesajViewModel();
            if (fileBases[0] == null && personel.Id == 0)
            {
                Response.Write("<script language='javascript'>alert(\"Lütfen resim seçiniz!\")</script>");
                var model = new PersonelAddViewModel()
                {
                    Departmanlar = list,
                    Personel = personel

                };
                return View("PersonelForm", model);
            }


            if (!ModelState.IsValid)
            {
                var model = new PersonelAddViewModel()
                {
                    Departmanlar = list,
                    Personel = personel

                };
                return View("PersonelForm", model);
            }
            if (personel.Id == 0 && fileBases[0] != null) //ekleme
            {
                p.Resim = new List<Resim>();
                foreach (var item in fileBases)
                {
                    Resim resimler = new Resim();
                    string resimadi = Path.GetFileName(item.FileName);
                    string resimyolu = "\\Image\\" + resimadi;
                    resimler.PathName = resimyolu;
                    item.SaveAs(Server.MapPath(resimyolu));
                    p.Resim.Add(resimler);


                }


                mesajModel.Status = true;

                db.Personel.Add(p);
                mesajModel.Mesaj = personel.Ad + " " + personel.Soyad + " personeli başarıyla eklendi!";
                mesajModel.LinkText = "Personel listesine dönmek için tıklayın.";
                mesajModel.Url = "/Personel";

            }
            else if (personel.Id != 0)//Güncelleme
            {
                personel.Resim = new List<Resim>();
                var silinecekResimler = db.Personel.Include("Resim").FirstOrDefault(x => x.Id == personel.Id);
                if (fileBases[0] == null) // kullanıcı resim seçmemişse
                {
                    silinecekResimler.Ad = personel.Ad;
                    silinecekResimler.Soyad = personel.Soyad;
                    silinecekResimler.Maas = personel.Maas;
                    silinecekResimler.Cinsiyet = personel.Cinsiyet;
                    silinecekResimler.DepartmanId = personel.DepartmanId;
                    silinecekResimler.EvliMi = personel.EvliMi;
                    silinecekResimler.DogumTarihi = personel.DogumTarihi;

                    mesajModel.Status = true;
                    mesajModel.Mesaj = personel.Ad + " " + personel.Soyad + " personeli başarıyla güncellendi";
                    mesajModel.LinkText = "Personel listesine dönmek için tıklayın.";
                    mesajModel.Url = "/Personel";
                }
                if (personel.DepartmanId != null && fileBases[0] != null)
                {
                    foreach (var item in silinecekResimler.Resim.ToList())
                    {
                        db.Resim.Remove(item); //önce var olan resimleri sil
                    }
                    silinecekResimler.Ad = personel.Ad;
                    silinecekResimler.Soyad = personel.Soyad;
                    silinecekResimler.Maas = personel.Maas;
                    silinecekResimler.Cinsiyet = personel.Cinsiyet;
                    silinecekResimler.DepartmanId = personel.DepartmanId;
                    silinecekResimler.EvliMi = personel.EvliMi;
                    silinecekResimler.DogumTarihi = personel.DogumTarihi;
                    silinecekResimler.Resim = personel.Resim;




                    foreach (var item in fileBases)
                    {
                        //sonra yeni resimleri ekle
                        Resim resimler = new Resim();
                        string resimadi = Path.GetFileName(item.FileName);
                        string resimyolu = "\\Image\\" + resimadi;
                        resimler.PathName = resimyolu;
                        item.SaveAs(Server.MapPath(resimyolu));
                        silinecekResimler.Resim.Add(resimler);


                    }


                    // db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
                    mesajModel.Status = true;
                    mesajModel.Mesaj = personel.Ad + " " + personel.Soyad + " personeli başarıyla güncellendi";
                    mesajModel.LinkText = "Personel listesine dönmek için tıklayın.";
                    mesajModel.Url = "/Personel";
                }

                else if (personel.DepartmanId == null)
                {
                    Response.Write("<script language='javascript'>alert(\"Lütfen departman seçiniz!\")</script>");
                    var model = new PersonelAddViewModel()
                    {
                        Departmanlar = list,
                        Personel = personel

                    };
                    return View("PersonelForm", model);
                }

            }

            db.SaveChanges();


            return View("_Mesaj", mesajModel);

        }
        [Authorize(Roles = "A")]
        public ActionResult Guncelle(int id)
        {
            var list = db.Departman.Select(x => new DepartmanListVM
            {
                Ad = x.Ad,
                Id = x.Id,
                Personel = x.Personel,
                PersonelSayisi = x.PersonelSayisi,
            }).ToList();

            Personel p = new Personel();
            p = db.Personel.Find(id);

            var model = new PersonelAddViewModel()
            {
                Departmanlar = list,
                Personel = new PersonelAddVM()
                {
                    Ad = p.Ad,
                    Soyad = p.Soyad,
                    Id = p.Id,
                    Cinsiyet = p.Cinsiyet,
                    Departman = p.Departman,
                    DepartmanId = p.DepartmanId,
                    DogumTarihi = p.DogumTarihi,
                    EvliMi = p.EvliMi,
                    Maas = p.Maas,
                    Resim = p.Resim
                    
                    
                    

                }
            };
            return View("PersonelForm", model);
        }

        [Authorize(Roles = "A")]
        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            //var silinecekResim = db.Resim.Find(id);
            var silinecekResimler = db.Personel.Include("Resim").FirstOrDefault(x => x.Id == id);
            if (silinecekResimler.Resim.Count()>0 || silinecekResimler.Resim ==null)
            {
                foreach (var item in silinecekResimler.Resim.ToList())
                {
                    db.Resim.Remove(item);
                }
            }
           
            if (silinecekPersonel == null)
            {
                return HttpNotFound();
            }
            else
            {

                db.Personel.Remove(silinecekPersonel);
            }

            db.SaveChanges();
            
            return RedirectToAction("Index", "Personel");
        }
        public ActionResult PersonelleriListele(int id)

        {
            Departman departman = db.Departman.FirstOrDefault(x => x.Id == id);
            ViewData["departmanAdi"] = departman.Ad;

            var model1 = db.Personel.Select(x => new PersonelListVM
            {

                Id = x.Id,
                Resimler = x.Resim.Select(a => a.PathName).ToList(),
                Soyad = x.Soyad,
                Ad = x.Ad,
                Departman = x.Departman,
                DepartmanId = x.DepartmanId,
                EvliMi = x.EvliMi,
                Maas = x.Maas,
                DogumTarihi = x.DogumTarihi,
                Cinsiyet = x.Cinsiyet,
                PersonelGorsel = x.PersonelGorsel,
            }).Where(x=>x.DepartmanId==id).ToList();



           // var model = db.Personel.Where(x => x.DepartmanId == id).ToList();

            return PartialView(model1);
        }
       
        public int? ToplamMaas()
        {
            return db.Personel.Sum(x => x.Maas);
        }

        [Authorize(Roles = "A")]
        public PartialViewResult StudentManager1()
        {

            var model = new NewPersonelVM()
            {
                DepartmanlarListesi = db.Departman.ToList(),

            };
            return PartialView("~/Views/Personel/_CokluPersonel.cshtml", model);
        }
        [Authorize(Roles = "A")]
        public ActionResult CreatePersonels()
        {
            
            

            return View(new MultiplePersonel()
            {
                // Personeller = new List<NewPersonelVM>(),
                Personeller = new List<NewPersonelVM>() { new NewPersonelVM { Ad = null,Cinsiyet=false,DepartmanId=null,EvliMi=false,DogumTarihi=null,
               Maas=null,Id=0,PersonelGorsel=null,Soyad=null,Resim=null,DepartmanlarListesi=db.Departman.ToList()} },


            });
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult CreatePersonels(MultiplePersonel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Personeller != null && model.Personeller.Count != 0)
                {
                    for (int i = 0; i < model.Personeller.Count; i++)
                    {
                        model.Personeller[i].Resim = new List<Resim>();
                        Resim resimler = new Resim();
                        string dosyaadi = Path.GetFileName(Request.Files[i].FileName);

                        if (dosyaadi != "")
                        {
                            string yol = "\\Image\\" + dosyaadi;
                            resimler.PathName = yol;
                            Request.Files[i].SaveAs(Server.MapPath(yol));
                            model.Personeller[i].Resim.Add(resimler);


                            
                        }
                        if (dosyaadi == "")
                        {
                            if (model.Personeller[i].Cinsiyet == true)
                            {
                                resimler.PathName = "\\Image\\" + "erkek.png";
                            }
                            else
                            {
                                resimler.PathName = "\\Image\\" + "kadın.png";
                            }
                            model.Personeller[i].Resim.Add(resimler);
                        }
                        Personel p = new Personel()
                        {
                            Id = model.Personeller[i].Id,
                            Ad = model.Personeller[i].Ad,
                            Soyad = model.Personeller[i].Soyad,
                            Cinsiyet = model.Personeller[i].Cinsiyet,
                            Departman = model.Personeller[i].Departman,
                            DepartmanId = model.Personeller[i].DepartmanId,
                            DogumTarihi = model.Personeller[i].DogumTarihi,
                            EvliMi = model.Personeller[i].EvliMi,
                            Maas = model.Personeller[i].Maas,
                            //Resimler = x.Resim.Select(a => a.PathName).ToList(),
                            Resim = model.Personeller[i].Resim,
                        };
                        db.Personel.Add(p);
                    }
                    // db.Personel.AddRange(model.Personeller);
                    Response.Write("<script language='javascript'>alert(\"Personeller başarıyla eklendi!\")</script>");
                }
                else
                {
                    Response.Write("<script language='javascript'>alert(\"Lütfen Personel Ekle butonuna basınız" +
                        " ve istenilen bilgileri doldurunuz!\")</script>");
                    return View(model);
                }

            }
            db.SaveChanges();
            //return RedirectToAction("Index", "Personel");
            return View(new MultiplePersonel());
        }
    }
}