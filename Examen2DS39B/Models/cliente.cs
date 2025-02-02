//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Examen2DS39B.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            this.cuenta = new HashSet<cuenta>();
        }

        
        [Display(Name = "C�digo cliente")]
        [Range(1, 1000000, ErrorMessage = "Ingrese un valor valido (1-1000000).")]
        [Required]
        public int cod_cliente { get; set; }

        
        [Display(Name = "Nombre cliente")]
        [RegularExpression(@"^[a-zA-Z .''-'\s]{1,40}$", ErrorMessage = "Solo se permiten letras y numeros")]
        [Required]
        public string nombre_cliente { get; set; }

        [Display(Name = "NIT")]
        [Required]
        public string nit { get; set; }

        [Display(Name = "Rol")]
        [Required]
        public string rol { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cuenta> cuenta { get; set; }
    }
}
