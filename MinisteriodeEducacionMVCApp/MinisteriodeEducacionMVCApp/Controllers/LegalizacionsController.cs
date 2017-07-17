using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinisteriodeEducacionMVCApp.Models;

namespace MinisteriodeEducacionMVCApp.Controllers
{
    public class LegalizacionsController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: Legalizacions
        public ActionResult Index()
        {
            var legalizacion = db.Legalizacion.Include(l => l.PersonalMinisterio);
            return View(legalizacion.ToList());
        }

        // GET: Legalizacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Legalizacion legalizacion = db.Legalizacion.Find(id);
            if (legalizacion == null)
            {
                return HttpNotFound();
            }
            return View(legalizacion);
        }

        // GET: Legalizacions/Create
        public ActionResult Create()
        {
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro");
            return View();
        }

        // POST: Legalizacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLegalizacion,estado,descripcion,firmaDigital,fechaL,nroRegistroMins")] Legalizacion legalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Legalizacion.Add(legalizacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", legalizacion.nroRegistroMins);
            return View(legalizacion);
        }

        // GET: Legalizacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Legalizacion legalizacion = db.Legalizacion.Find(id);
            if (legalizacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", legalizacion.nroRegistroMins);
            return View(legalizacion);
        }

        // POST: Legalizacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLegalizacion,estado,descripcion,firmaDigital,fechaL,nroRegistroMins")] Legalizacion legalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(legalizacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", legalizacion.nroRegistroMins);
            return View(legalizacion);
        }

        // GET: Legalizacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Legalizacion legalizacion = db.Legalizacion.Find(id);
            if (legalizacion == null)
            {
                return HttpNotFound();
            }
            return View(legalizacion);
        }

        // POST: Legalizacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Legalizacion legalizacion = db.Legalizacion.Find(id);
            db.Legalizacion.Remove(legalizacion);
            db.SaveChanges();
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
