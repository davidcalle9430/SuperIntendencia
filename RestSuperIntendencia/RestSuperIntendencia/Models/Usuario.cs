using RestSuperIntendencia.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestSuperIntendencia.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }


        [Display(Name = "Número Documento")]
        [Index("documento", 1, IsUnique = true)]
        [StringLength( 100 )]
        public string NumeroDocumento { get; set; }


        [Display(Name = "Tipo Documento")]
        [Index("documento", 2, IsUnique = true)]
        [StringLength( 100 )]
        public string TipoDocumento { get; set; }
        //public virtual List< Transaccion > Transacciones { get; set; }
    }
}