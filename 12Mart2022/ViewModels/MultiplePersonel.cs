using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class MultiplePersonel
    {
  

        public IEnumerable<Departman> Departmanlar { get; set; }

        public List<NewPersonelVM> Personeller { get; set; }

        public NewPersonelVM Personel { get; set; }

        public Departman Departman { get; set; }


    }
}