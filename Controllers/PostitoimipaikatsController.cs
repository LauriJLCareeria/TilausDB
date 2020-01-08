using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;

namespace WebAppTilausDB.Controllers
{
    public class PostitoimipaikatsController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Postitoimipaikats
        public ActionResult Index()
        {
            if (Session["Käyttäjätunnus"] == null && Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                return View(db.Postitoimipaikat.ToList());
            }

        }

        // GET: Postitoimipaikats/Details
        public ActionResult Details(string id)
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
                Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
                if (postitoimipaikat == null)
                {
                    return HttpNotFound();
                }
                return View(postitoimipaikat);
            }
        }

        // GET: Postitoimipaikats/Create
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

        // POST: Postitoimipaikats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat postitoimipaikat)
        {
            if (ModelState.IsValid)
            {
                db.Postitoimipaikat.Add(postitoimipaikat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postitoimipaikat);
        }

        // GET: Postitoimipaikats/Edit
        public ActionResult Edit(string id)
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
                Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
                if (postitoimipaikat == null)
                {
                    return HttpNotFound();
                }
                return View(postitoimipaikat);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat postitoimipaikat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postitoimipaikat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postitoimipaikat);
        }

        // GET: Postitoimipaikats/Delete
        public ActionResult Delete(string id)
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
                Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
                if (postitoimipaikat == null)
                {
                    return HttpNotFound();
                }
                return View(postitoimipaikat);
            }
        }

        // POST: Postitoimipaikats/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
            db.Postitoimipaikat.Remove(postitoimipaikat);
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
