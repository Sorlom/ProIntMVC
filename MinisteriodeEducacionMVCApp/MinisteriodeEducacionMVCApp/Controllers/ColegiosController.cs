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
    public class ColegiosController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: Colegios
        public ActionResult Index()
        {
            var colegio = db.Colegio.Include(c => c.PersonalMinisterio);
            return View(colegio.ToList());
        }

        // GET: Colegios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colegio colegio = db.Colegio.Find(id);
            if (colegio == null)
            {
                return HttpNotFound();
            }
            return View(colegio);
        }

        // GET: Colegios/Create
        public ActionResult Create()
        {
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro");
            return View();
        }

        // POST: Colegios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idColegio,nombre,direccion,correo,nroRegistroMins")] Colegio colegio)
        {
            if (ModelState.IsValid)
            {
                db.Colegio.Add(colegio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", colegio.nroRegistroMins);
            return View(colegio);
        }

        // GET: Colegios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colegio colegio = db.Colegio.Find(id);
            if (colegio == null)
            {
                return HttpNotFound();
            }
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", colegio.nroRegistroMins);
            return View(colegio);
        }

        // POST: Colegios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idColegio,nombre,direccion,correo,nroRegistroMins")] Colegio colegio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colegio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", colegio.nroRegistroMins);
            return View(colegio);
        }

        // GET: Colegios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colegio colegio = db.Colegio.Find(id);
            if (colegio == null)
            {
                return HttpNotFound();
            }
            return View(colegio);
        }

        // POST: Colegios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Colegio colegio = db.Colegio.Find(id);
            db.Colegio.Remove(colegio);
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
