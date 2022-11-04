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
    public class DepartmanController : Controller
    {
        // GET: Departman
        masterEntities db = new masterEntities();


        [Authorize(Roles = "A,B")]
        public ActionResult Index()
        {
            // var model = db.Departman.ToList();



            var model = db.Departman.Select(p => new DepartmanCreateVM
            {
                Ad = p.Ad,
                Id = p.Id,
                PersonelSayisi = p.Personel.Count(),


            }).ToList();

            return View(model);

        }

        [Authorize(Roles = "A")]
        [HttpGet]

        public ActionResult Yeni()
        {
           

            var model = new Departman
            {
                Personel = new List<Personel> { new Personel { Ad = "", Soyad = "", Cinsiyet = false, DepartmanId = 0, DogumTarihi = DateTime.Today, EvliMi = false, Id = 0, Maas = 0 } }
            };

            return View("DepartmanForm", model);
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult Kaydet(Departman departman)
        {

            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            MesajViewModel model = new MesajViewModel();
            bool durum = db.Departman.Any(_departman => _departman.Ad.Equals(departman.Ad));

            if (departman.Id == 0 && durum == false)
            {
                // db.Departman.Add(departman);
                model.Status = true;
                model.Mesaj = departman.Ad + " departmanı başarıyla eklendi!";


            }
            else if (departman.Id == 0 && durum == true)
            {
                model.Status = false;
                model.Mesaj = departman.Ad + " isminde departman zaten var. Ekleme Başarısız!";

            }
            else if (departman.Id != 0)
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman != null)
                {
                    guncellenecekDepartman.Ad = departman.Ad;
                    model.Status = true;
                    model.Mesaj = departman.Ad + " başarıyla güncellendi!";
                }
                else
                {

                    model.Status = true;
                    model.Mesaj = departman.Ad + "Güncelleme başarısız!";
                }

            }
            db.SaveChanges();

            model.LinkText = "Departman listesine dönmek için tıklayın.";
            model.Url = "/Departman";
            return View("_Mesaj", model);

        }

        [Authorize(Roles = "A")]
        public ActionResult Guncelle(int id)
        {
            Departman p = new Departman();
            p = db.Departman.Find(id);

            var liste = db.Personel.Where(x => x.DepartmanId == id).Select(per => new PersonelUpdateVM
            {
                Id = per.Id,
                Resimler = per.Resim.Select(a => a.PathName).ToList(),
                Soyad = per.Soyad,
                Ad = per.Ad,
                Departman = per.Departman,
                DepartmanId = per.DepartmanId,
                EvliMi = per.EvliMi,
                Maas = per.Maas,
                DogumTarihi = per.DogumTarihi,
                Cinsiyet = per.Cinsiyet,
                PersonelGorsel = per.PersonelGorsel,

            }).ToList();
            var model1 = new PersonelUpdateViewModel()
            {

                Departman = new DepartmanUpdateVM
                {
                    DepartmanAd = p.Ad,
                    DepartmanId = p.Id,

                },
                Personels = liste,
            };

            var model = db.Departman.FirstOrDefault(x => x.Id == id);
            var model3 = db.Personel.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return HttpNotFound();
            ViewBag.Kontrol = 0;
            return View("DepartmanPersonelGuncelle", model1);
        }


        [Authorize(Roles = "A")]
        public ActionResult Sil(int id)
        {
            // bool durum = db.Departman.Any(_departman => _departman.Ad.Equals(departman.Ad));


            var silinecekDepartman = db.Departman.Find(id);
            int personelSayi = silinecekDepartman.Personel.Count();
            //ViewData["departmanAdi"] = departman.Ad;


            if (silinecekDepartman == null || personelSayi != 0)
            {
                return HttpNotFound();
            }
            else if (silinecekDepartman != null && personelSayi == 0)
            {
                db.Departman.Remove(silinecekDepartman);
            }


            db.SaveChanges();
            return RedirectToAction("Index", "Departman");

        }
        public int? perSayisi()
        {

            return db.Personel.Count();
        }
        public int? derSayisi()
        {

            return db.Departman.Count();
        }
        [Authorize(Roles = "A")]
        public ActionResult AddNewPerson()
        {
            //var employee = new Employee();
            // return PartialView("_Employee", employee);
            var personel = new Personel();
            return PartialView("_Personel", personel);
        }

        [Authorize(Roles = "A")]
        public PartialViewResult StudentManager()
        {
            return PartialView("~/Views/Personel/_Personel.cshtml", new NewPersonelVM());
        }
        [Authorize(Roles = "A")]
        public ActionResult Create()
        {


            return View(new DepartmanFormViewModel()
            {
                Personels = new List<NewPersonelVM>(),
                Departman = new NewDepartmanVM()


            });
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult Create(DepartmanFormViewModel employee)
        {

            Departman d = new Departman()
            {
                Id = employee.Departman.Id,
                Ad = employee.Departman.Ad,
                Personel = employee.Departman.Personel,
                PersonelSayisi = employee.Departman.PersonelSayisi
            };

            bool durum = db.Departman.Any(x => x.Ad.Equals(employee.Departman.Ad));
            int sayac = 0;
            if (employee.Departman.Id == 0) // ekleme 
            {
                if (ModelState.IsValid && durum == false)
                {

                    db.Departman.Add(d);

                    var model = db.Departman.Find(0);

                    if (employee.Personels != null && employee.Personels.Count != 0)
                    {
                        for (int i = 0; i < employee.Personels.Count; i++)
                        {

                            employee.Personels[i].Resim = new List<Resim>();
                            Resim resimler = new Resim();
                            string dosyaadi = Path.GetFileName(Request.Files[i].FileName);

                            employee.Personels[i].DepartmanId = model.Id;
                            sayac++;

                            if (dosyaadi != "")
                            {
                                string yol = "\\Image\\" + dosyaadi;
                                Request.Files[i].SaveAs(Server.MapPath(yol));
                                resimler.PathName = yol;
                                employee.Personels[i].Resim.Add(resimler);
                            }
                            if (dosyaadi == "")
                            {
                                if (employee.Personels[i].Cinsiyet == true)
                                {
                                    resimler.PathName = "\\Image\\" + "erkek.png";
                                }
                                else
                                {
                                    resimler.PathName = "\\Image\\" + "kadın.png";
                                }
                                employee.Personels[i].Resim.Add(resimler);
                            }
                            Personel p = new Personel()
                            {
                                Id = employee.Personels[i].Id,
                                Ad = employee.Personels[i].Ad,
                                Soyad = employee.Personels[i].Soyad,
                                Cinsiyet = employee.Personels[i].Cinsiyet,
                                Departman = employee.Personels[i].Departman,
                                DepartmanId = employee.Personels[i].DepartmanId,
                                DogumTarihi = employee.Personels[i].DogumTarihi,
                                EvliMi = employee.Personels[i].EvliMi,
                                Maas = employee.Personels[i].Maas,
                                //Resimler = x.Resim.Select(a => a.PathName).ToList(),
                                Resim = employee.Personels[i].Resim,
                            };
                            db.Personel.Add(p);
                        }

                    }

                    if (sayac == 0)
                    {

                        Response.Write("<script language='javascript'>alert(\"Departman ekleme işleminiz başarıyla tamamlandı\")</script>");
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert(\"Departman ve Personel ekleme işleminiz başarıyla tamamlandı\")</script>");
                    }




                }
                else if (ModelState.IsValid && durum == true)
                {
                    Response.Write("<script language='javascript'>alert(\"Başarısız! Lütfen farklı bir departman adı giriniz\")</script>");
                }
            }
            else //güncelleme
            {
                MesajViewModel model = new MesajViewModel();
                if (employee.Personels == null || employee.Personels.Count() == 0)//sadece dep. güncelleme
                {
                    var guncellenecekDepartman = db.Departman.Find(employee.Departman.Id);
                    guncellenecekDepartman.Ad = employee.Departman.Ad;
                    Response.Write("<script language='javascript'>alert(\"Departman başarıyla güncellendi!\")</script>");
                }
                else if (employee.Personels != null && employee.Personels.Count != 0 && employee.Personels[0].Id == 0)//dep güncellerken personel de ekleme
                {
                    var guncellenecekDepartman = db.Departman.Find(employee.Departman.Id);
                    guncellenecekDepartman.Ad = employee.Departman.Ad;
                    for (int i = 0; i < employee.Personels.Count; i++)
                    {
                        employee.Personels[i].Resim = new List<Resim>();
                        Resim resimler = new Resim();
                        string dosyaadi = Path.GetFileName(Request.Files[i].FileName);

                        employee.Personels[i].DepartmanId = guncellenecekDepartman.Id;

                        sayac++;

                        if (dosyaadi != "")
                        {
                            string yol = "\\Image\\" + dosyaadi;
                            Request.Files[i].SaveAs(Server.MapPath(yol));
                            resimler.PathName = yol;
                            employee.Personels[i].Resim.Add(resimler);
                        }
                        if (dosyaadi == "")
                        {
                            if (employee.Personels[i].Cinsiyet == true)
                            {
                                resimler.PathName = "\\Image\\" + "erkek.png";
                            }
                            else
                            {
                                resimler.PathName = "\\Image\\" + "kadın.png";
                            }
                            employee.Personels[i].Resim.Add(resimler);
                        }
                    }
                    // db.Personel.AddRange(person);

                    Response.Write("<script language='javascript'>alert(\"Ekleme / Güncelleme işlemi başarıyla tamamlandı!\")</script>");
                }
                else //hem dep hem personel güncelleme 
                {
                    var guncellenecekDepartman = db.Departman.Find(employee.Departman.Id);
                    guncellenecekDepartman.Ad = employee.Departman.Ad;
                    for (int i = 0; i < employee.Personels.Count(); i++)
                    {
                        var guncellenecekPersonel = db.Personel.Find(employee.Personels[i].Id);


                        employee.Personels[i].Resim = new List<Resim>();
                        Resim resimler = new Resim();
                        string dosyaadi = Path.GetFileName(Request.Files[i].FileName);

                        if (dosyaadi != "")
                        {
                            foreach (var item in guncellenecekPersonel.Resim.ToList())
                            {
                                db.Resim.Remove(item);
                            }
                            string yol = "\\Image\\" + dosyaadi;
                            Request.Files[i].SaveAs(Server.MapPath(yol));
                            resimler.PathName = yol;
                            employee.Personels[i].Resim.Add(resimler);

                        }
                        

                        guncellenecekPersonel.Ad = employee.Personels[i].Ad;
                        guncellenecekPersonel.Soyad = employee.Personels[i].Soyad;
                        guncellenecekPersonel.Maas = employee.Personels[i].Maas;
                        guncellenecekPersonel.DogumTarihi = employee.Personels[i].DogumTarihi;
                        guncellenecekPersonel.Cinsiyet = employee.Personels[i].Cinsiyet;
                        guncellenecekPersonel.EvliMi = employee.Personels[i].EvliMi;

                    }
                    Response.Write("<script language='javascript'>alert(\"Departman ve Personeller başarıyla güncellendi!\")</script>");

                }


            }

            db.SaveChanges();

           
            return View(employee);
        }
        [Authorize(Roles = "A")]
        public ActionResult DepartmanPersonelGuncelle()
        {


            return View(new PersonelUpdateViewModel()
            {
                Personels = new List<PersonelUpdateVM>(),


            });
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult DepartmanPersonelGuncelle(PersonelUpdateViewModel employee)
        {
            bool durum = db.Departman.Any(x => x.Ad.Equals(employee.Departman.DepartmanAd));
            MesajViewModel model = new MesajViewModel();
            if (employee.Personels == null || employee.Personels.Count() == 0)//sadece dep. güncelleme
            {
                var guncellenecekDepartman = db.Departman.Find(employee.Departman.DepartmanId);
                if (guncellenecekDepartman.Ad == employee.Departman.DepartmanAd)
                {
                    guncellenecekDepartman.Ad = employee.Departman.DepartmanAd;
                    Response.Write("<script language='javascript'>alert(\"Departman başarıyla güncellendi!\")</script>");
                }
                else
                {
                    if (durum == false)
                    {
                        guncellenecekDepartman.Ad = employee.Departman.DepartmanAd;
                        Response.Write("<script language='javascript'>alert(\"Departman başarıyla güncellendi!\")</script>");
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert(\"Başarısız! Aynı isimde Departman zaten var!\")</script>");
                    }
                }

                
            }
            else if (employee.Personels != null && employee.Personels.Count != 0 && employee.Personels[0].Id == 0)//dep güncellerken personel de ekleme
            {
                var guncellenecekDepartman = db.Departman.Find(employee.Departman.DepartmanId);
                guncellenecekDepartman.Ad = employee.Departman.DepartmanAd;
                for (int i = 0; i < employee.Personels.Count; i++)
                {
                    employee.Personels[i].Resim = new List<Resim>();
                    Resim resimler = new Resim();
                    string dosyaadi = Path.GetFileName(Request.Files[i].FileName);

                    employee.Personels[i].DepartmanId = guncellenecekDepartman.Id;


                    if (dosyaadi != "")
                    {
                        string yol = "\\Image\\" + dosyaadi;
                        Request.Files[i].SaveAs(Server.MapPath(yol));
                        resimler.PathName = yol;
                        employee.Personels[i].Resim.Add(resimler);
                    }
                    if (dosyaadi == "")
                    {
                        if (employee.Personels[i].Cinsiyet == true)
                        {
                            resimler.PathName = "\\Image\\" + "erkek.png";
                        }
                        else
                        {
                            resimler.PathName = "\\Image\\" + "kadın.png";
                        }
                        employee.Personels[i].Resim.Add(resimler);
                    }
                }
                //db.Personel.AddRange(employee.Personels);

                Response.Write("<script language='javascript'>alert(\"Ekleme / Güncelleme işlemi başarıyla tamamlandı!\")</script>");
            }
            else //hem dep hem personel güncelleme 
            {

                var guncellenecekDepartman = db.Departman.Find(employee.Departman.DepartmanId);
                if (guncellenecekDepartman.Ad == employee.Departman.DepartmanAd)
                {
                    guncellenecekDepartman.Ad = employee.Departman.DepartmanAd;

                }
                else
                {
                    if (durum == false)
                    {
                        guncellenecekDepartman.Ad = employee.Departman.DepartmanAd;

                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert(\"Başarısız!  Aynı isimde Departman zaten var!\")</script>");
                        return View(employee);
                    }
                }

                for (int i = 0; i < employee.Personels.Count(); i++)
                {
                    var guncellenecekPersonel = db.Personel.Find(employee.Personels[i].Id);


                    employee.Personels[i].Resim = new List<Resim>();
                    Resim resimler = new Resim();
                    string dosyaadi = Path.GetFileName(Request.Files[i].FileName);

                    if (dosyaadi != "")
                    {
                        foreach (var item in guncellenecekPersonel.Resim.ToList())
                        {
                            db.Resim.Remove(item);
                        }
                        string yol = "\\Image\\" + dosyaadi;
                        Request.Files[i].SaveAs(Server.MapPath(yol));
                        resimler.PathName = yol;
                        guncellenecekPersonel.Resim.Add(resimler);

                    }

                    guncellenecekPersonel.Ad = employee.Personels[i].Ad;
                    guncellenecekPersonel.Soyad = employee.Personels[i].Soyad;
                    guncellenecekPersonel.Maas = employee.Personels[i].Maas;
                    guncellenecekPersonel.DogumTarihi = employee.Personels[i].DogumTarihi;
                    guncellenecekPersonel.Cinsiyet = employee.Personels[i].Cinsiyet;
                    guncellenecekPersonel.EvliMi = employee.Personels[i].EvliMi;

                }
                Response.Write("<script language='javascript'>alert(\"Departman ve Personeller başarıyla güncellendi!\")</script>");

            }

            db.SaveChanges();

            return View(employee);
        }
        public ActionResult NewPersonOnDepartman(int id)
        {
            Departman d = new Departman();
            d = db.Departman.Find(id);

            var model1 = new NewPersonToDepartmanViewModel()
            {
                Departman = new NewDepartmanVM()
                {
                    Ad = d.Ad,
                    Id = d.Id,
                    Personel = d.Personel,
                    PersonelSayisi = d.PersonelSayisi
                },
                Personels = new List<NewPersonelVM>() { new NewPersonelVM { Ad = null,Cinsiyet=false,DepartmanId=null,EvliMi=false,DogumTarihi=null,
               Maas=null,Id=0,PersonelGorsel=null,Soyad=null,Resim=null,DepartmanlarListesi=db.Departman.ToList()} },
            };
            return View("AddPersonel", model1);
        }
        public ActionResult AddPersonel()
        {

            return View(new NewPersonToDepartmanViewModel()
            {
                Personels = new List<NewPersonelVM>(),

            });
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult AddPersonel(NewPersonToDepartmanViewModel employee)
        {
            if (ModelState.IsValid)
            {


                if (employee.Personels != null && employee.Personels.Count != 0)
                {
                    var model = db.Departman.Find(employee.Departman.Id);
                    for (int i = 0; i < employee.Personels.Count; i++)
                    {
                        employee.Personels[i].Resim = new List<Resim>();
                        Resim resimler = new Resim();
                        string dosyaadi = Path.GetFileName(Request.Files[i].FileName);

                        employee.Personels[i].DepartmanId = model.Id;


                        if (dosyaadi != "")
                        {
                            string yol = "\\Image\\" + dosyaadi;
                            Request.Files[i].SaveAs(Server.MapPath(yol));
                            resimler.PathName = yol;
                            employee.Personels[i].Resim.Add(resimler);
                        }
                        if (dosyaadi == "")
                        {
                            if (employee.Personels[i].Cinsiyet == true)
                            {
                                resimler.PathName = "\\Image\\" + "erkek.png";
                            }
                            else
                            {
                                resimler.PathName = "\\Image\\" + "kadın.png";
                            }
                            employee.Personels[i].Resim.Add(resimler);
                        }
                        Personel p = new Personel()
                        {
                            Id = employee.Personels[i].Id,
                            Ad = employee.Personels[i].Ad,
                            Soyad = employee.Personels[i].Soyad,
                            Cinsiyet = employee.Personels[i].Cinsiyet,
                            Departman = employee.Personels[i].Departman,
                            DepartmanId = employee.Personels[i].DepartmanId,
                            DogumTarihi = employee.Personels[i].DogumTarihi,
                            EvliMi = employee.Personels[i].EvliMi,
                            Maas = employee.Personels[i].Maas,
                            //Resimler = x.Resim.Select(a => a.PathName).ToList(),
                            Resim = employee.Personels[i].Resim,
                        };
                        db.Personel.Add(p);
                    }

                    Response.Write("<script language='javascript'>alert(\"Personeller başarıyla eklendi!\")</script>");
                }
                else
                {
                    Response.Write("<script language='javascript'>alert(\"Lütfen Personel Formu Ekle Butonuna basınız\")</script>");
                }

            }
            db.SaveChanges();

            return View(new NewPersonToDepartmanViewModel());
        }


    }
}