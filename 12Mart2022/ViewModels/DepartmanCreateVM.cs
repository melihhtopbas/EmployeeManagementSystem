using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class DepartmanCreateVM
    {
        // [Display(Name = "Departman Adı")]
        // [Required(ErrorMessage = "Departman adı girmek zorunludur!")]

        [Display(Name = "Departman Adı")]
        [Required(ErrorMessage = "Departman adı girmek zorunludur! Lütfen!")]
        public string Ad { get; set; }

        public int Id { get; set; }

        public int PersonelSayisi { get; set; }

        public List<Personel> Personels { get; set; }

    }
}