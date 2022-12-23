using B2C.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2C.ViewModel
{
    public class ProductViewModel
    {
        //public IEnumerable<NewDepartmanVM> Departmanlar { get; set; }

        //public List<NewPersonelVM> Personels { get; set; }

        //public NewPersonelVM Personel { get; set; }

        //public NewDepartmanVM Departman { get; set; }
        public Products Products { get; set; }

        public Varyants Varyantlar { get; set; }
        public List<VaryantDegerler> VaryantlarDegeris { get; set; }


        //public IEnumerable<DepartmanListVM> Departmanlar { get; set; }
        public IEnumerable<HazırVaryant> hazırVaryants { get; set; }

        public HazırVaryant HazırVaryant { get; set; }
      //  public IEnumerable<HazırDeger> hazırDegers { get; set; }
        public List<HazırDeger> hazırDegers { get; set; }

        public HazırDeger HazırDeger { get; set; }
        public VaryantDegerler VaryantDegerler { get; set; }



    }
}