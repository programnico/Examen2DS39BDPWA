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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class transacciones
    {

        [Display(Name = "C�digo transacci�n")]
        public int cod_transac { get; set; }

        [Required]
        [Display(Name = "Monto")]
        [Range(1.0, 1000000.0, ErrorMessage = "Ingrese un valor valido (1.0-1000000.0).")]
        public Nullable<double> monto { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string tipo { get; set; }

        [Required]
        [Display(Name = "N�mero cuenta")]
        public Nullable<int> ncta { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fecha { get; set; }
    
        public virtual cuenta cuenta { get; set; }
    }
}
