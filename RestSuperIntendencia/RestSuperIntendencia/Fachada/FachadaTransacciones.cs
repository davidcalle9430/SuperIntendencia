using RestSuperIntendencia.Controllers;
using RestSuperIntendencia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace RestSuperIntendencia.Fachada
{
    public class FachadaTransacciones
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public FachadaTransacciones()
        {

        }

        public IQueryable<Transaccion> darTransacciones()
        {
            return db.Transaccions;
        }
        public IQueryable<Transaccion> darTransaccionesUsuario( int idUsuario )
        {
            return db.Transaccions.Where( t => t.UsuarioId == idUsuario );
        }

        public Transaccion buscarTransaccion( int id)
        {
            return db.Transaccions.Find(id); 
        }

        public Transaccion actualizarTransaccion( Transaccion transaccion)
        {
            db.Entry(transaccion).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return transaccion;
        }

        public bool agregarTransaccion( Transaccion transaccion)
        {
            db.Transaccions.Add(transaccion);
            db.SaveChanges();
            return true;
        }
        
        public bool eliminarTransaccion( Transaccion transaccion)
        {
            db.Transaccions.Remove(transaccion);
            db.SaveChanges();
            return true;
        }

    }
}