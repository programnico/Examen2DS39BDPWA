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

    public partial class cuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cuenta()
        {
            this.transacciones = new HashSet<transacciones>();
        }

        [Display(Name = "C�digo cuenta")]
        [Range(1, 1000000, ErrorMessage = "Ingrese un valor valido (1-1000000).")]
        [Required]
        public int ncta { get; set; }
        public Nullable<double> saldo { get; set; }
        public Nullable<int> cod_cliente { get; set; }
    
        public virtual cliente cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transacciones> transacciones { get; set; }
    }
}
