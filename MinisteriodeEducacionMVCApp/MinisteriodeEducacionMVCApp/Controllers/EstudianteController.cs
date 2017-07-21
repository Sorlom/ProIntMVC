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
    [Authorize]
    public class EstudianteController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        [Authorize(Roles = "R1,R2,R3")]
        // GET: Estudiante
        public ActionResult Index()
        {
            if (TempData["DErr"] != null)
            {
                ViewBag.Msg = TempData["DErr"].ToString();
            }
            var estudiante = db.Estudiante.Include(e => e.ListadeEstudiantes).Include(e => e.Rol).Include(e => e.Persona);
            return View(estudiante.ToList());
        }
        [Authorize(Roles = "R1,R2,R3")]
        // GET: Estudiante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }
        [AllowAnonymous]
        // GET: Estudiante/Create
        public ActionResult Create()
        {
            ViewBag.idListaEstudiante = new SelectList(db.ListadeEstudiantes, "idListaEstudiante", "correo");
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "nombre",3);
            ViewBag.CI = new SelectList(db.Persona, "CI", "CI");
            return View();
        }
        [AllowAnonymous]
        // POST: Estudiante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nroRegistroEst,loginEstudiante,passEstudiante,correo,CI,idListaEstudiante,idRol")] Estudiante estudiante)
        {
            try
            {
                var correoIng = estudiante.correo;
                var correoGuar = db.ListadeEstudiantes.Where(x => x.correo == correoIng).FirstOrDefault().correo;
                if (correoIng == correoGuar)
                {
                    if (ModelState.IsValid)
                    {
                        db.Estudiante.Add(estudiante);
                        db.SaveChanges();
                        TempData["DErr"] = "Registro Exitoso";
                        return RedirectToAction("Index","Login");
                    }

                    ViewBag.idListaEstudiante = new SelectList(db.ListadeEstudiantes, "idListaEstudiante", "nombre", estudiante.idListaEstudiante);
                    ViewBag.idRol = new SelectList(db.Rol, "idRol", "nombre", estudiante.idRol);
                    ViewBag.CI = new SelectList(db.Persona, "CI", "nombre", estudiante.CI);
                    return View(estudiante);
                }
                else
                {
                    TempData["DErr"] = "Los Correos Deben ser iguales: Es estudiante no se encuentra en una lista";
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception e)
            {
                TempData["DErr"] = "Error de Datos al Crear Estudiante";
                return RedirectToAction("Index", "Login");
            }
        }
        [Authorize(Roles = "R1")]
        // GET: Estudiante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            ViewBag.idListaEstudiante = new SelectList(db.ListadeEstudiantes, "idListaEstudiante", "nombre", estudiante.idListaEstudiante);
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "nombre", estudiante.idRol);
            ViewBag.CI = new SelectList(db.Persona, "CI", "CI", estudiante.CI);
            return View(estudiante);
        }
        [Authorize(Roles = "R1")]
        // POST: Estudiante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nroRegistroEst,loginEstudiante,passEstudiante,correo,CI,idListaEstudiante,idRol")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idListaEstudiante = new SelectList(db.ListadeEstudiantes, "idListaEstudiante", "nombre", estudiante.idListaEstudiante);
            ViewBag.idRol = new SelectList(db.Rol, "idRol", "nombre", estudiante.idRol);
            ViewBag.CI = new SelectList(db.Persona, "CI", "CI", estudiante.CI);
            return View(estudiante);
        }
        [Authorize(Roles = "R1")]
        // GET: Estudiante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }
        [Authorize(Roles = "R1")]
        // POST: Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiante estudiante = db.Estudiante.Find(id);
            db.Estudiante.Remove(estudiante);
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
