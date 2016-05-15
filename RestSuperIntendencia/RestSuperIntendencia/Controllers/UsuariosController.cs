using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using RestSuperIntendencia.Models;
using RestSuperIntendencia.Fachada;

namespace RestSuperIntendencia.Controllers
{
    public class UsuariosController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private FachadaUsuario fachada = new FachadaUsuario();
        // GET: api/Usuarios
        public IQueryable<Usuario> GetUsuarios()
        {
            return fachada.darUsuarios();
        }


        public IQueryable<Usuario> GetUsuarios( int pageIndex , int pageSize)
        {
            return fachada.obtenerUsuarios( pageSize , pageIndex );
        }



        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario usuario = fachada.buscarUsuario( id );
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario( int id , Usuario usuario )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            var edicion = fachada.editarUsuario(usuario);
            if( edicion == false)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            fachada.agregarUsuario(usuario);
            return CreatedAtRoute("DefaultApi", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            var resultado = fachada.eliminarUsuario(id);
            if( resultado == null )
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            return Ok(resultado);
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.Count(e => e.Id == id) > 0;
        }*/
    }
}