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
    public class PerfilController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: Perfil
        public ActionResult Index()
        {
            var perfil = db.Vista_Perfil;
            //return View(perfil.ToList());
            var idper = TempData["IDlogin"];
            return RedirectToAction("Details", "Perfil", new { id = idper} );
        }

        [HttpGet]
        // GET: Perfil/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //Estudiante estudiante = db.Estudiante.Find(id);
                //Vista_Perfil perfil = db.Vista_Perfil.Find(id);
                Vista_Perfil perfil = db.Vista_Perfil.Where(x => x.id == id).FirstOrDefault();
                if (perfil == null)
                {
                    return HttpNotFound();
                }
                return View(perfil);
            }
            catch (Exception e)
            {
                return RedirectToAction("Nocert", "Perfil", new { id = id });
            }
        }
        [HttpGet]
        public ActionResult Nocert(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //Estudiante estudiante = db.Estudiante.Find(id);
                //Vista_Perfil perfil = db.Vista_Perfil.Find(id);
                Vista_NoPerfil perfil = db.Vista_NoPerfil.Where(x => x.id == id).FirstOrDefault();
                if (perfil == null)
                {
                    return HttpNotFound();
                }
                return View(perfil);
            }
            catch (Exception e)
            {
                TempData["CER"] = "Usted no cuenta con un Diploma";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
