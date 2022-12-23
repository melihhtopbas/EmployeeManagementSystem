using B2C.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2C.ViewModel
{
    public class ProductListVM
    {
        public ProductListVM()
        {
            this.VaryantlarDegeri = new HashSet<VaryantDegerler>();
        }

        public int VaryantId { get; set; }
        public string VaryantName { get; set; }
        public Nullable<int> ProductId { get; set; }

        public virtual Products Products { get; set; }
        public virtual ICollection<VaryantDegerler> VaryantlarDegeri { get; set; }

        public List<string> VaryantValues = new List<string>();
    }
}