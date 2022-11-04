using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class DepartmanFormViewModel
    {
        public IEnumerable<NewDepartmanVM> Departmanlar { get; set; }

        public List<NewPersonelVM> Personels { get; set; }

        public NewPersonelVM Personel { get; set; }

        public NewDepartmanVM Departman { get; set; }


    }
}