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
    
    public partial class VaryantDegerler
    {
        public int VaryantDegerId { get; set; }
        public string VaryantValue { get; set; }
        public Nullable<int> VaryantId { get; set; }
    
        public virtual Varyants Varyants { get; set; }
    }
}