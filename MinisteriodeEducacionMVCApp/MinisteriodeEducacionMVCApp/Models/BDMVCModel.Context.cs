﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MinisteriodeEducacionMVCApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProIntBDEntities : DbContext
    {
        public ProIntBDEntities()
            : base("name=ProIntBDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Colegio> Colegio { get; set; }
        public virtual DbSet<Diploma> Diploma { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<Gestion> Gestion { get; set; }
        public virtual DbSet<GrupoDiploma> GrupoDiploma { get; set; }
        public virtual DbSet<Legalizacion> Legalizacion { get; set; }
        public virtual DbSet<ListadeEstudiantes> ListadeEstudiantes { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<PersonalColegio> PersonalColegio { get; set; }
        public virtual DbSet<PersonalMinisterio> PersonalMinisterio { get; set; }
        public virtual DbSet<Privilegios> Privilegios { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Vista_PC_Rol> Vista_PC_Rol { get; set; }
    }
}
