﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VaryantlarEntities : DbContext
    {
        public VaryantlarEntities()
            : base("name=VaryantlarEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<HazırDeger> HazırDeger { get; set; }
        public DbSet<HazırVaryant> HazırVaryant { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<VaryantDegerler> VaryantDegerler { get; set; }
        public DbSet<Varyants> Varyants { get; set; }
    }
}
