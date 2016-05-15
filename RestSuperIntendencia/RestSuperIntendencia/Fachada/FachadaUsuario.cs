using RestSuperIntendencia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace RestSuperIntendencia.Fachada
{
    public class FachadaUsuario
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<Usuario> darUsuarios()
        {
            return db.Usuarios;
        }

        public Usuario buscarUsuario( int id)
        {
            return db.Usuarios.Find(id);
        }

        public bool editarUsuario( Usuario usuario)
        {
            db.Entry(usuario).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public Usuario eliminarUsuario( int idUsuario)
        {
            Usuario usuario = buscarUsuario( idUsuario );
            if (usuario == null)
            {
                return null;
            }
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return usuario;
        }

        public bool agregarUsuario( Usuario usuario)
        {
            db.Usuarios.Add(usuario);
            db.SaveChanges();
            return true;
        }
    }
}