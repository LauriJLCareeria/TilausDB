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
    public class TilauksetController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Tilaukset
        public ActionResult Index()
        {
            if (Session["Käyttäjätunnus"] == null && Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                var tilaukset = db.Tilaukset.Include(t => t.Postitoimipaikat);
                return View(tilaukset.ToList());
            }
        }

        // GET: Tilaukset/Details
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
                {
                    return HttpNotFound();
                }
                return View(tilaukset);
            }
        }

        // GET: Tilaukset/Create
        public ActionResult Create()
        {
            if (Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi");
                ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi");

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

        // POST: Tilaukset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,TuoteID,Maara,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(tilaukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postinumero", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilaukset/Edit
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
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

                ViewBag.Postinumero = new SelectList(postitmplista, "Value", "Text", tilaukset.Postinumero);
                return View(tilaukset);
            }
        }

        // POST: Tilaukset/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postinumero", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilaukset/Delete
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
                {
                    return HttpNotFound();
                }
                return View(tilaukset);
            }
        }

        // POST: Tilaukset/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaukset);
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
