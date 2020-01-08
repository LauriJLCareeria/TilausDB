using System;
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
    public class TuotteetController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Tuotteet
        public ActionResult Index()
        {
            if (Session["Käyttäjätunnus"] == null && Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                return View(db.Tuotteet.ToList());
            }            
        }

        public ActionResult Index2()
        {
            if (Session["Käyttäjätunnus"] == null && Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                return View(db.Tuotteet.ToList());
            }             
        }

        // GET: Tuotteet/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Create
        public ActionResult Create()
        {
            if (Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                return View();
            }               
        }

        // POST: Tuotteet/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Ahinta,KuvaLinkki")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Tuotteet.Add(tuotteet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Edit
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
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                if (tuotteet == null)
                {
                    return HttpNotFound();
                }
                return View(tuotteet);
            }
        }

        // POST: Tuotteet/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,Ahinta,KuvaLinkki")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuotteet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Delete/5
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
                Tuotteet tuotteet = db.Tuotteet.Find(id);
                if (tuotteet == null)
                {
                    return HttpNotFound();
                }
                return View(tuotteet);
            }
        }

        // POST: Tuotteet/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            db.Tuotteet.Remove(tuotteet);
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
