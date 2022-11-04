using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class PersonelUpdateViewModel
    {
        public IEnumerable<DepartmanUpdateVM> Departmanlar { get; set; }

        public List<PersonelUpdateVM> Personels { get; set; }

        public PersonelUpdateVM Personel { get; set; }

        public DepartmanUpdateVM Departman { get; set; }

      
    }
}