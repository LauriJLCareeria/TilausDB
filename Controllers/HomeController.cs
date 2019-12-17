﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;

namespace WebAppTilausDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Vähätyö Global Enterprises Plc.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Löydät meidät täältä:";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            TilausDBEntities1 db = new TilausDBEntities1();
            var LoggedUser = db.Logins.SingleOrDefault(x => x.Käyttäjätunnus == LoginModel.Käyttäjätunnus && x.Salasana == LoginModel.Salasana); 
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Onnistunut kirjautuminen";
                ViewBag.LoggedStatus = "Kirjautunut";
                Session["Käyttäjätunnus"] = LoggedUser.Käyttäjätunnus;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Kirjautuminen epäonnistui";
                ViewBag.LoggedStatus = "Ei kirjautunut";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana."; 
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Ei kirjautunut";
            return RedirectToAction("Index", "Home");
        }
    }
}