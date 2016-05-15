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
    public class TransaccionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProxyTransacciones proxy = new ProxyTransacciones();
        private ProxyUsuario proxyUsuarios = new ProxyUsuario();
        // GET: Transacciones
        public ActionResult Index()
        {
            var transaccions = proxy.darTransacciones();
            return View(transaccions.ToList());
        }

        public ActionResult Usuario( int id )
        {
            var transacciones = proxy.darTransaccionesUsuario(id);
            return View( "Index" ,  transacciones );
        }

        // GET: Transacciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = proxy.buscarTransaccion(id.Value);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // GET: Transacciones/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList( proxyUsuarios.darUsuarios( 1 , 1000 ) , "Id", "Nombre");
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Concepto,Valor,UsuarioId,Fecha")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                proxy.crearTransaccion(transaccion);
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList( proxyUsuarios.darUsuarios(1, 1000) , "Id" , "Nombre" , transaccion.UsuarioId );
            return View(transaccion);
        }

        // GET: Transacciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = proxy.buscarTransaccion(id.Value);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioId = new SelectList(proxyUsuarios.darUsuarios(1, 1000), "Id", "Nombre", transaccion.UsuarioId);
            return View(transaccion);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Concepto,Valor,UsuarioId,Fecha")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                proxy.EditarTransaccion(transaccion);
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioId = new SelectList( proxyUsuarios.darUsuarios(1, 1000) , "Id", "Nombre", transaccion.UsuarioId);
            return View(transaccion);
        }

        // GET: Transacciones/Delete/5
        public ActionResult Delete( int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = proxy.buscarTransaccion( id.Value );
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaccion transaccion = proxy.buscarTransaccion( id );
            proxy.EliminarTransaccion( id );
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
