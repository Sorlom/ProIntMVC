using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinisteriodeEducacionMVCApp.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using LinqToExcel;
using System.Data.Entity.Validation;

namespace MinisteriodeEducacionMVCApp.Controllers
{
    [Authorize(Roles = "R1,R2")]
    public class ListadeEstudiantesController : Controller
    {


        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: ListadeEstudiantes
        public ActionResult Index()
        {
            if (TempData["DErr2"] != null)
            {
                ViewBag.Msg = TempData["DErr2"].ToString();
            }
            if (TempData["Excel"] != null)
            {
                ViewBag.Msg = TempData["Excel"].ToString();
            }
            var listadeEstudiantes = db.ListadeEstudiantes.Include(l => l.Diploma).Include(l => l.Gestion).Include(l => l.GrupoDiploma);
            return View(listadeEstudiantes.ToList());
        }

        public FileResult DownloadExcel()
        {
            string path = "/excelfolder/ListaEstudiantes.xlsx";
            return File(path, "application/vnd.ms-excel", "ListaEstudiantes.xlsx");
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
            ViewBag.idDiploma = new SelectList(db.Diploma, "idDiploma", "idDiploma");
            ViewBag.idGestion = new SelectList(db.Gestion, "idGestion", "nombrePromo");
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre");
            return View();
        }

        // POST: ListadeEstudiantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idListaEstudiante,nombre,apellidoPaterno,apellidoMaterno,correo,paralelo,promedio,idGrupoDiploma,idGestion,idDiploma")] ListadeEstudiantes listadeEstudiantes)
        {
            if (ModelState.IsValid)
            {
                db.ListadeEstudiantes.Add(listadeEstudiantes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDiploma = new SelectList(db.Diploma, "idDiploma", "metadatos", listadeEstudiantes.idDiploma);
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
            ViewBag.idDiploma = new SelectList(db.Diploma, "idDiploma", "idDiploma", listadeEstudiantes.idDiploma);
            ViewBag.idGestion = new SelectList(db.Gestion, "idGestion", "nombrePromo", listadeEstudiantes.idGestion);
            ViewBag.idGrupoDiploma = new SelectList(db.GrupoDiploma, "idGrupoDiploma", "nombre", listadeEstudiantes.idGrupoDiploma);
            return View(listadeEstudiantes);
        }

        // POST: ListadeEstudiantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idListaEstudiante,nombre,apellidoPaterno,apellidoMaterno,correo,paralelo,promedio,idGrupoDiploma,idGestion,idDiploma")] ListadeEstudiantes listadeEstudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listadeEstudiantes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDiploma = new SelectList(db.Diploma, "idDiploma", "idDiploma", listadeEstudiantes.idDiploma);
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
            var Del = db.Estudiante.Where(x => x.idListaEstudiante == id).Count();
            if (Del == 1)
            {
                TempData["DErr2"] = "Estudiante con Login en la APP: Borrar primero Cuenta de Estudiante";
                return RedirectToAction("Index");
            }
            else
            {
                ListadeEstudiantes listadeEstudiantes = db.ListadeEstudiantes.Find(id);
                db.ListadeEstudiantes.Remove(listadeEstudiantes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult ImportarLista()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportarLista(ListadeEstudiantes users, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {


                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/excelfolder/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Hoja1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Hoja1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<ListadeEstudiantes>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.nombre != "" && a.apellidoPaterno != "" && a.apellidoMaterno != "" && a.correo != "" && a.paralelo != "" && a.promedio != 0
                                && a.idGrupoDiploma != 0 && a.idGestion != 0)
                            {
                                ListadeEstudiantes TU = new ListadeEstudiantes();
                                TU.nombre = a.nombre;
                                TU.apellidoPaterno = a.apellidoPaterno;
                                TU.apellidoMaterno = a.apellidoMaterno;
                                TU.correo = a.correo;
                                TU.paralelo = a.paralelo;
                                TU.promedio = a.promedio;
                                TU.idGrupoDiploma = a.idGrupoDiploma;
                                TU.idGestion = a.idGestion;
                                TU.idDiploma = null;
                                db.ListadeEstudiantes.Add(TU);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                data.Add("<li>Los valores de archivo estan mal</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                // return Json(data, JsonRequestBehavior.AllowGet);
                                TempData["Excel"] = "Los valores del archivo se encuentran mal";
                                return RedirectToAction("Index");
                            }
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Propiedad: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            data.Add("<ul>");
                            data.Add("<li>Error</li>");
                            data.Add("</ul>");
                            data.ToArray();
                            //return Json(data, JsonRequestBehavior.AllowGet);
                            TempData["Excel"] = "Datos Ingresados no Permitidos: Correo debe ser unico";
                            return RedirectToAction("Index");
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    //return Json("success", JsonRequestBehavior.AllowGet);
                    TempData["Excel"] = "Datos Importados Exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Solo archivo Excel Permitidos</li>");
                    data.Add("</ul>");
                    data.ToArray();
                   // return Json(data, JsonRequestBehavior.AllowGet);
                    TempData["Excel"] = "Solo archivos Excel permitidos";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Por favor eliga un archivo Excel</li>");
                data.Add("</ul>");
                data.ToArray();
                TempData["Excel"] = "Elegir un archivo excel";
                return RedirectToAction("Index");
                //return Json(data, JsonRequestBehavior.AllowGet);
            }
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
