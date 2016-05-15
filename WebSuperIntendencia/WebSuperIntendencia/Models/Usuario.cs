using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSuperIntendencia.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public virtual List<Transaccion> Transacciones { get; set; }
    }
}