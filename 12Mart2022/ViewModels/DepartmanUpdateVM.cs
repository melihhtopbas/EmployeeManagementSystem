using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class DepartmanUpdateVM
    {
        [Display(Name = "Please Enter Department Name")]
        public string DepartmanAd { get; set; }
        public int DepartmanId { get; set; }
        public virtual ICollection<Personel> Personel { get; set; }

        public List<PersonelUpdateVM> Personels { get; set; }
    }
}