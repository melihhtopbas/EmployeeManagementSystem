using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class PersonelAddViewModel
    {
        public IEnumerable<DepartmanListVM> Departmanlar { get; set; }

        public List<PersonelAddVM> Personels { get; set; }

        public PersonelAddVM Personel { get; set; }

        public DepartmanListVM Departman { get; set; }
    }
}