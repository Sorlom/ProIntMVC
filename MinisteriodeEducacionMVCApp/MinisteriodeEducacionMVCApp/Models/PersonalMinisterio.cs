//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class PersonalMinisterio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonalMinisterio()
        {
            this.Colegio = new HashSet<Colegio>();
            this.Legalizacion = new HashSet<Legalizacion>();
        }
    
        public int nroRegistroMins { get; set; }
        public string loginMinistro { get; set; }
        public string passMinistro { get; set; }
        public string correo { get; set; }
        public string firmaDigital { get; set; }
        public int CI { get; set; }
        public int idRol { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Colegio> Colegio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Legalizacion> Legalizacion { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
