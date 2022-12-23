using B2C.Models.EntityFramework;
using B2C.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B2C.Controllers
{
    public class HomeController : Controller
    {
        VaryantlarEntities db = new VaryantlarEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Ekle()
        {

            //return View(new DepartmanFormViewModel()
            //{
            //    Personels = new List<NewPersonelVM>(),
            //    Departman = new NewDepartmanVM()


            //});
            var list = db.HazırVaryant.ToList();
            var list1 = db.HazırDeger.ToList();
            return View(new ProductViewModel()
            {
                Products = new Products(),
                Varyantlar = new Varyants(),
                VaryantlarDegeris = new List<VaryantDegerler>(),
                hazırVaryants = list,
                hazırDegers = list1

            });

        }
        [HttpPost]
        public ActionResult Ekle(ProductViewModel productViewModel)
        {
            Products products = new Products()
            {
                ProductId = productViewModel.Products.ProductId,
                ProductName = productViewModel.Products.ProductName
            };
            //   var model1 = db.HazırVaryant.Find(productViewModel.HazırVaryant.HazırVaryantId);

            Varyants varyantlar = new Varyants()
            {
                // VaryantId = productViewModel.Varyantlar.VaryantId,
                //VaryantName = productViewModel.Varyantlar.VaryantName,
                ProductId = products.ProductId,
                HazırVaryantId = productViewModel.Varyantlar.HazırVaryantId
            };


            for (int i = 0; i < productViewModel.VaryantlarDegeris.Count; i++)
            {
                VaryantDegerler varyantlarDegeri = new VaryantDegerler()
                {
                    VaryantDegerId = productViewModel.VaryantlarDegeris[i].VaryantDegerId,
                    VaryantValue = productViewModel.VaryantlarDegeris[i].VaryantValue,
                    VaryantId = varyantlar.VaryantId,
                };
                db.VaryantDegerler.Add(varyantlarDegeri);
            }
            // varyantlar.VaryantlarDegeri.Add(varyantlarDegeri);



            db.Products.Add(products);
            db.Varyants.Add(varyantlar);

            db.SaveChanges();
            return RedirectToAction("Liste");

        }

        public PartialViewResult Create()
        {
            return PartialView("~/Views/Home/_Variant.cshtml", new VaryantDegerler());
        }
        public PartialViewResult Value()
        {

            var list1 = db.HazırDeger.ToList();
            return PartialView("~/Views/Home/_Value.cshtml", new ProductViewModel() { hazırDegers = list1});
        }
        public ActionResult Liste()
        {
         

            var model = db.Varyants.ToList();
            return View(model);
        }
        public JsonResult GetProducts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<HazırDeger> list = db.HazırDeger.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }





    }
}



