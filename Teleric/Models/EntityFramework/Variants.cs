//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teleric.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Variants
    {
        public Variants()
        {
            this.VariantsOptions = new HashSet<VariantsOptions>();
        }
    
        public int PropId { get; set; }
        public string PropName { get; set; }
    
        public virtual ICollection<VariantsOptions> VariantsOptions { get; set; }
    }
}