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
    [Authorize(Roles = "R1,R4")]
    public class LegalizacionController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: Legalizacion
        public ActionResult Index()
        {
            var legalizacion = db.Legalizacion.Include(l => l.Diploma).Include(l => l.PersonalMinisterio);
            return View(legalizacion.ToList());
        }

        // GET: Legalizacion/Details/5
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

        // GET: Legalizacion/Create
        public ActionResult Create()
        {
            ViewBag.idDiploma = new SelectList(db.Diploma, "idDiploma", "idDiploma");
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro");
            return View();
        }

        // POST: Legalizacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLegalizacion,firmaDigital,fechaL,nroRegistroMins,idDiploma")] Legalizacion legalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Legalizacion.Add(legalizacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDiploma = new SelectList(db.Diploma, "idDiploma", "idDiploma", legalizacion.idDiploma);
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", legalizacion.nroRegistroMins);
            return View(legalizacion);
        }

        // GET: Legalizacion/Edit/5
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
            ViewBag.idDiploma = new SelectList(db.Diploma, "idDiploma", "idDiploma", legalizacion.idDiploma);
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", legalizacion.nroRegistroMins);
            return View(legalizacion);
        }

        // POST: Legalizacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLegalizacion,firmaDigital,fechaL,nroRegistroMins,idDiploma")] Legalizacion legalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(legalizacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDiploma = new SelectList(db.Diploma, "idDiploma", "idDiploma", legalizacion.idDiploma);
            ViewBag.nroRegistroMins = new SelectList(db.PersonalMinisterio, "nroRegistroMins", "loginMinistro", legalizacion.nroRegistroMins);
            return View(legalizacion);
        }

        // GET: Legalizacion/Delete/5
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

        // POST: Legalizacion/Delete/5
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
