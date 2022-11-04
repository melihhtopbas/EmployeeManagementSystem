using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class NewDepartmanVM
    {
        public NewDepartmanVM()
        {
            this.Personel = new HashSet<Personel>();
        }

        public int Id { get; set; }
        [Display(Name = "Department Name ")]
        [Required(ErrorMessage ="Please Enter Department Name!")]
        public string Ad { get; set; }
        public Nullable<int> PersonelSayisi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personel> Personel { get; set; }
    }
}