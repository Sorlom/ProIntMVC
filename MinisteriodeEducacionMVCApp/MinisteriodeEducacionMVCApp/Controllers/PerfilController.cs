using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinisteriodeEducacionMVCApp.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

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
        [HttpPost]
        public ActionResult Details(int id)
        {
            try
            {
                if (id == 0)
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
                DownloadPDF();
                return View(perfil);
            
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
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
        public byte[] GetPDF(string pHTML)
        {
            byte[] bPDF = null;

            MemoryStream ms = new MemoryStream();

            TextReader txtReader = new StringReader(pHTML);

            // 1: create object of a itextsharp document class
            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            // 3: we create a worker parse the document
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document
            doc.Open();
            htmlWorker.StartDocument();

            // 5: parse the html into the document
            htmlWorker.Parse(txtReader);

            // 6: close the document and the worker
            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

            bPDF = ms.ToArray();

            return bPDF;
        }
        public void DownloadPDF()
        {
            string HTMLContent = "Hello <b>World</b>";

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + ""+TempData["login"]+"PDFfile.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(GetPDF(HTMLContent));
            Response.End();
        }

    }
}
