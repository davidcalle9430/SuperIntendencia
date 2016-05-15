using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSuperIntendencia.Models;
using WebSuperIntendencia.ProxyRS;

namespace WebSuperIntendencia.Controllers
{
    public class UsuariosController : Controller
    {
        private ProxyUsuario proxyUsuario = new ProxyUsuario();
        // GET: Usuarios
      /*  public ActionResult Index()
        {
            return View( proxyUsuario.darUsuarios() );
        }*/

        public ActionResult Index( int pagina = 1 , int tamPagina = 5)
        {
            if( pagina <= 0)
            {
                pagina = 1;
            }
            ViewBag.pagina = pagina;
            ViewBag.tamPagina = tamPagina;
            return View(proxyUsuario.darUsuarios(pagina, tamPagina));
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = proxyUsuario.buscarUsuario( id.Value );
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NumeroDocumento,TipoDocumento")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var resultado = proxyUsuario.crearUsaurio(usuario);
                if ( resultado == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = proxyUsuario.buscarUsuario( id.Value );
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NumeroDocumento,TipoDocumento")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var resultado = proxyUsuario.EditarUsuario( usuario );
                if( resultado == false )
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Usuario usuario = proxyUsuario.buscarUsuario( id.Value );
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            proxyUsuario.EliminarUsuario( id );
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            /*
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
            */
        }
    }
}
