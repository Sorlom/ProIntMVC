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
    [Authorize(Roles = "R1,R2")]
    public class DiplomataController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: Diplomata
        public ActionResult Index()
        {
            var diploma = db.Diploma.Include(d => d.GrupoDiploma).Include(d => d.Legalizacion);
            return View(diploma.ToList());
        }

        // GET: Diplomata/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diploma diploma = db.Diploma.Find(id);
            if (diploma == null)
            {
                return HttpNotFound();
            }
            return View(diploma);
        }

        // GET: Diplomata/Create
        public ActionResult Create()
        {
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre");
            ViewBag.idLegalizacion = new SelectList(db.Legalizacion, "idLegalizacion", "estado");
            return View();
        }

        // POST: Diplomata/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDiploma,fecha,metadatos,codigohex,idLegalizacion,codigoLegalizacion,idGrupoDiploma")] Diploma diploma)
        {
            if (ModelState.IsValid)
            {
                db.Diploma.Add(diploma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", diploma.idGrupoDiploma);
            ViewBag.idLegalizacion = new SelectList(db.Legalizacion, "idLegalizacion", "estado", diploma.idLegalizacion);
            return View(diploma);
        }

        // GET: Diplomata/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diploma diploma = db.Diploma.Find(id);
            if (diploma == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", diploma.idGrupoDiploma);
            ViewBag.idLegalizacion = new SelectList(db.Legalizacion, "idLegalizacion", "estado", diploma.idLegalizacion);
            return View(diploma);
        }

        // POST: Diplomata/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDiploma,fecha,metadatos,codigohex,idLegalizacion,codigoLegalizacion,idGrupoDiploma")] Diploma diploma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diploma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", diploma.idGrupoDiploma);
            ViewBag.idLegalizacion = new SelectList(db.Legalizacion, "idLegalizacion", "estado", diploma.idLegalizacion);
            return View(diploma);
        }

        // GET: Diplomata/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diploma diploma = db.Diploma.Find(id);
            if (diploma == null)
            {
                return HttpNotFound();
            }
            return View(diploma);
        }

        // POST: Diplomata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diploma diploma = db.Diploma.Find(id);
            db.Diploma.Remove(diploma);
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
