﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB;
using WebAppTilausDB.Models;

namespace WebAppTilausDB.Controllers
{
    public class AsiakkaatController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Asiakkaat
        public ActionResult Index()
        {
            if (Session["Käyttäjätunnus"] == null && Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                var asiakkaat = db.Asiakkaat.Include(a => a.Postitoimipaikat);
                return View(asiakkaat.ToList());
            }          
        }

        // GET: Asiakkaat/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Käyttäjätunnus"] == null && Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                if (asiakkaat == null)
                {
                    return HttpNotFound();
                }
                return View(asiakkaat);
            }

        }

        // GET: Asiakkaat/Create
        public ActionResult Create()
        {
            if (Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                List<SelectListItem> postitmplista = new List<SelectListItem>();
                foreach (Postitoimipaikat postitmp in db.Postitoimipaikat)
                {
                    postitmplista.Add(new SelectListItem
                    {
                        Value = postitmp.Postinumero.ToString(),
                        Text = postitmp.Postinumero.ToString() + " " + postitmp.Postitoimipaikka
                    });
                }

                ViewBag.Postinumero = new SelectList(postitmplista, "Value", "Text", null);
                return View();
            }              
        }

        // POST: Asiakkaat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero,Postitoimipaikka")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaat.Add(asiakkaat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postinumero", asiakkaat.Postinumero);
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                if (asiakkaat == null)
                {
                    return HttpNotFound();
                }

                List<SelectListItem> postitmplista = new List<SelectListItem>();
                foreach (Postitoimipaikat postitmp in db.Postitoimipaikat)
                {
                    postitmplista.Add(new SelectListItem
                    {
                        Value = postitmp.Postinumero.ToString(),
                        Text = postitmp.Postinumero.ToString() + " " + postitmp.Postitoimipaikka
                    });
                }

                ViewBag.Postinumero = new SelectList(postitmplista, "Value", "Text", asiakkaat.Postinumero);
                return View(asiakkaat);
            }
        }

        // POST: Asiakkaat/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero,Postitoimipaikka")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakkaat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postinumero", asiakkaat.Postinumero);
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Delete
        public ActionResult Delete(int? id)
        {
            if (Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
                if (asiakkaat == null)
                {
                    return HttpNotFound();
                }
                return View(asiakkaat);
            }
        }

        // POST: Asiakkaat/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(asiakkaat);
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
