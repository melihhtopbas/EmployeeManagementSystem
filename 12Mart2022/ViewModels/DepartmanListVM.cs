using _12Mart2022.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _12Mart2022.ViewModels
{
    public class DepartmanListVM
    {
        public DepartmanListVM()
        {
            this.Personel = new HashSet<Personel>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }
        public Nullable<int> PersonelSayisi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personel> Personel { get; set; }
    }
}