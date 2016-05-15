using RestSuperIntendencia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSuperIntendencia.Controllers
{
    public class Transaccion
    {

        public Transaccion()
        {
            Fecha = DateTime.Now;
        }
        public int Id { get; set; }
        public string Concepto { get; set; }
        public double Valor { get; set; }
        public int UsuarioId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }


        public virtual Usuario Usuario { get; set; }
    }
}
