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
    public class DiplomaController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: Diploma
        public ActionResult Index()
        {
            var diploma = db.Diploma.Include(d => d.GrupoDiploma);
            return View(diploma.ToList());
        }
        [HttpPost]
        public ActionResult Index(int? id)
        {
            //var diploma = db.Diploma.Include(d => d.GrupoDiploma);
            var lista = db.Database.SqlQuery<Diploma>("sp_CrearDiplomas").ToList();
            return View(lista);
        }

        // GET: Diploma/Details/5
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

        // GET: Diploma/Create
        public ActionResult Create()
        {
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre");
            return View();
        }

        // POST: Diploma/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDiploma,fecha,metadatos,codigohex,codigoLegalizacion,idListaEstudiante,idGrupoDiploma")] Diploma diploma)
        {
            if (ModelState.IsValid)
            {
                db.Diploma.Add(diploma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", diploma.idGrupoDiploma);
            return View(diploma);
        }

        // GET: Diploma/Edit/5
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
            return View(diploma);
        }

        // POST: Diploma/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDiploma,fecha,metadatos,codigohex,codigoLegalizacion,idListaEstudiante,idGrupoDiploma")] Diploma diploma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diploma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", diploma.idGrupoDiploma);
            return View(diploma);
        }

        // GET: Diploma/Delete/5
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

        // POST: Diploma/Delete/5
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
