//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _12Mart2022.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resim
    {
        public int Id { get; set; }
        public string PathName { get; set; }
        public Nullable<int> ImageId { get; set; }
    
        public virtual Personel Personel { get; set; }
    }
}
