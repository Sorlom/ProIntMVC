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
    public class GrupoDiplomasController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: GrupoDiplomas
        public ActionResult Index()
        {
            var grupoDiploma = db.GrupoDiploma.Include(g => g.PersonalColegio);
            return View(grupoDiploma.ToList());
        }

        // GET: GrupoDiplomas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoDiploma grupoDiploma = db.GrupoDiploma.Find(id);
            if (grupoDiploma == null)
            {
                return HttpNotFound();
            }
            return View(grupoDiploma);
        }

        // GET: GrupoDiplomas/Create
        public ActionResult Create()
        {
            ViewBag.nroRegistroPColegio = new SelectList(db.PersonalColegio, "nroRegistroPColegio", "loginPColegio");
            return View();
        }

        // POST: GrupoDiplomas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idGrupoDiploma,nombre,nroRegistroPColegio")] GrupoDiploma grupoDiploma)
        {
            if (ModelState.IsValid)
            {
                db.GrupoDiploma.Add(grupoDiploma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nroRegistroPColegio = new SelectList(db.PersonalColegio, "nroRegistroPColegio", "loginPColegio", grupoDiploma.nroRegistroPColegio);
            return View(grupoDiploma);
        }

        // GET: GrupoDiplomas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoDiploma grupoDiploma = db.GrupoDiploma.Find(id);
            if (grupoDiploma == null)
            {
                return HttpNotFound();
            }
            ViewBag.nroRegistroPColegio = new SelectList(db.PersonalColegio, "nroRegistroPColegio", "loginPColegio", grupoDiploma.nroRegistroPColegio);
            return View(grupoDiploma);
        }

        // POST: GrupoDiplomas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idGrupoDiploma,nombre,nroRegistroPColegio")] GrupoDiploma grupoDiploma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupoDiploma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nroRegistroPColegio = new SelectList(db.PersonalColegio, "nroRegistroPColegio", "loginPColegio", grupoDiploma.nroRegistroPColegio);
            return View(grupoDiploma);
        }

        // GET: GrupoDiplomas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoDiploma grupoDiploma = db.GrupoDiploma.Find(id);
            if (grupoDiploma == null)
            {
                return HttpNotFound();
            }
            return View(grupoDiploma);
        }

        // POST: GrupoDiplomas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GrupoDiploma grupoDiploma = db.GrupoDiploma.Find(id);
            db.GrupoDiploma.Remove(grupoDiploma);
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
