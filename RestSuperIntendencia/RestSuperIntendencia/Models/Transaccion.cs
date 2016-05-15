using RestSuperIntendencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSuperIntendencia.Controllers
{
    public class Transaccion
    {
        public int Id { get; set; }
        public string Concepto { get; set; }
        public double Valor { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
