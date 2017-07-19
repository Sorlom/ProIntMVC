using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinisteriodeEducacionMVCApp.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace MinisteriodeEducacionMVCApp.Controllers
{
    public class LoginController : Controller
    {
        private ProIntBDEntities db = new ProIntBDEntities();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string acc = fc["Nom"].ToString();
            string pass = GetMD5(fc["Pass"].ToString());

            var countPC = db.PersonalColegio.Where(x => x.loginPColegio == acc && x.passPColegio == pass).Count();
            var countPM = db.PersonalMinisterio.Where(x => x.loginMinistro == acc && x.passMinistro == pass).Count();
            var countPE = db.Estudiante.Where(x => x.loginEstudiante == acc && x.passEstudiante == pass).Count();

            if (countPC == 1 || countPE == 1 || countPM == 1)
            {
                FormsAuthentication.SetAuthCookie(acc, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Msg = "Usuario Invalido";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        public string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}