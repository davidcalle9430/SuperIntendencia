using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestSuperIntendencia.Models;
using RestSuperIntendencia.Fachada;

namespace RestSuperIntendencia.Controllers
{
    [RoutePrefix("api/Transaccions")]
    public class TransaccionsController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private FachadaTransacciones fachada = new FachadaTransacciones();

        // GET: api/Transaccions
        public IQueryable<Transaccion> GetTransaccions()
        {
            return fachada.darTransacciones();
        }

        [HttpGet]
        [Route("Usuario/{id}")]
        public IQueryable<Transaccion> darTransaccionesUsuario( int id )
        {
            return fachada.darTransaccionesUsuario( id );
        }


        // GET: api/Transaccions/5
        [ResponseType(typeof(Transaccion))]
        public IHttpActionResult GetTransaccion(int id)
        {
            Transaccion transaccion = fachada.buscarTransaccion(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return Ok(transaccion);
        }

        // PUT: api/Transaccions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaccion( int id , Transaccion transaccion )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaccion.Id)
            {
                return BadRequest();
            }

            var t = fachada.actualizarTransaccion(transaccion);

            if( t == null)
            {
                return BadRequest();
            }

            return StatusCode( HttpStatusCode.OK );
        }

        // POST: api/Transaccions
        [ResponseType(typeof(Transaccion))]
        public IHttpActionResult PostTransaccion(Transaccion transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            fachada.agregarTransaccion(transaccion);

            return CreatedAtRoute("DefaultApi", new { id = transaccion.Id }, transaccion);
        }

        // DELETE: api/Transaccions/5
        [ResponseType(typeof(Transaccion))]
        public IHttpActionResult DeleteTransaccion(int id)
        {
            Transaccion transaccion = fachada.buscarTransaccion( id );
            if (transaccion == null)
            {
                return NotFound();
            }

            var res = fachada.eliminarTransaccion(transaccion);
            return Ok(transaccion);
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

        private bool TransaccionExists(int id)
        {
            return db.Transaccions.Count(e => e.Id == id) > 0;
        }*/
    }
}