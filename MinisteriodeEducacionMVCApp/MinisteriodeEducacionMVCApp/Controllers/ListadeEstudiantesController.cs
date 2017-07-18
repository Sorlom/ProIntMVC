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
    public class ListadeEstudiantesController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: ListadeEstudiantes
        public ActionResult Index()
        {
            var listadeEstudiantes = db.ListadeEstudiantes.Include(l => l.Gestion).Include(l => l.GrupoDiploma);
            return View(listadeEstudiantes.Take(10).ToList());
        }

        // GET: ListadeEstudiantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadeEstudiantes listadeEstudiantes = db.ListadeEstudiantes.Find(id);
            if (listadeEstudiantes == null)
            {
                return HttpNotFound();
            }
            return View(listadeEstudiantes);
        }

        // GET: ListadeEstudiantes/Create
        public ActionResult Create()
        {
            ViewBag.idGestion = new SelectList(db.Gestion, "idGestion", "nombrePromo");
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre");
            return View();
        }

        // POST: ListadeEstudiantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idListaEstudiante,nombre,apellidoPaterno,apellidoMaterno,correo,pararelo,promedio,idGrupoDiploma,idGestion")] ListadeEstudiantes listadeEstudiantes)
        {
            if (ModelState.IsValid)
            {
                db.ListadeEstudiantes.Add(listadeEstudiantes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idGestion = new SelectList(db.Gestion, "idGestion", "nombrePromo", listadeEstudiantes.idGestion);
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", listadeEstudiantes.idGrupoDiploma);
            return View(listadeEstudiantes);
        }

        // GET: ListadeEstudiantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadeEstudiantes listadeEstudiantes = db.ListadeEstudiantes.Find(id);
            if (listadeEstudiantes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idGestion = new SelectList(db.Gestion, "idGestion", "nombrePromo", listadeEstudiantes.idGestion);
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", listadeEstudiantes.idGrupoDiploma);
            return View(listadeEstudiantes);
        }

        // POST: ListadeEstudiantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idListaEstudiante,nombre,apellidoPaterno,apellidoMaterno,correo,pararelo,promedio,idGrupoDiploma,idGestion")] ListadeEstudiantes listadeEstudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listadeEstudiantes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idGestion = new SelectList(db.Gestion, "idGestion", "nombrePromo", listadeEstudiantes.idGestion);
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", listadeEstudiantes.idGrupoDiploma);
            return View(listadeEstudiantes);
        }

        // GET: ListadeEstudiantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListadeEstudiantes listadeEstudiantes = db.ListadeEstudiantes.Find(id);
            if (listadeEstudiantes == null)
            {
                return HttpNotFound();
            }
            return View(listadeEstudiantes);
        }

        // POST: ListadeEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListadeEstudiantes listadeEstudiantes = db.ListadeEstudiantes.Find(id);
            db.ListadeEstudiantes.Remove(listadeEstudiantes);
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
