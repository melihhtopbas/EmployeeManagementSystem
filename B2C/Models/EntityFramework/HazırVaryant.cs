//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace B2C.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class HazırVaryant
    {
        public HazırVaryant()
        {
            this.HazırDeger = new HashSet<HazırDeger>();
            this.Varyants = new HashSet<Varyants>();
        }
    
        public int HazırVaryantId { get; set; }
        public string HazırVaryantName { get; set; }
    
        public virtual ICollection<HazırDeger> HazırDeger { get; set; }
        public virtual ICollection<Varyants> Varyants { get; set; }
    }
}
