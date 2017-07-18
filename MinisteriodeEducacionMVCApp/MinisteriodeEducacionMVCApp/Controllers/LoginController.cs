using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinisteriodeEducacionMVCApp.Models;
using System.Web.Security;

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
        public ActionResult Index(PersonalColegio user)
        {
            var count = db.PersonalColegio.Where(x => x.loginPColegio == user.loginPColegio && x.passPColegio == user.passPColegio).Count();
            if (count == 0)
            {
                ViewBag.Msg = "Usuario Invalido";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.loginPColegio, false);
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}