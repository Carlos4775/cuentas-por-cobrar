﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppCxC
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BaseDatosCxCEntities : DbContext
    {
        public BaseDatosCxCEntities()
            : base("name=BaseDatosCxCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Balance> Balances { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tipo_Documentos> Tipo_Documentos { get; set; }
        public virtual DbSet<Transaccione> Transacciones { get; set; }
    }
}