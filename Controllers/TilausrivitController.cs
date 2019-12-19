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
    public class TilausrivitController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Tilausrivit
        public ActionResult Index()
        {
            if (Session["Käyttäjätunnus"] == null && Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                return View(db.Tilausrivit.ToList());
            }
            
        }

        // GET: Tilausrivit/Details/5
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
                Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
                if (tilausrivit == null)
                {
                    return HttpNotFound();
                }
                return View(tilausrivit);
            }
        }

        // GET: Tilausrivit/Create
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit tilausrivit)
        {
            if (ModelState.IsValid)
            {
                db.Tilausrivit.Add(tilausrivit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tilausrivit);
        }

        // GET: Tilausrivit/Edit/5
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
                Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
                if (tilausrivit == null)
                {
                    return HttpNotFound();
                }
                return View(tilausrivit);
            }
        }

        // POST: Tilausrivit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit tilausrivit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilausrivit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tilausrivit);
        }

        // GET: Tilausrivit/Delete/5
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
                Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
                if (tilausrivit == null)
                {
                    return HttpNotFound();
                }
                return View(tilausrivit);
            }
        }

        // POST: Tilausrivit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["KäyttäjätunnusAdmin"] == null && Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
                db.Tilausrivit.Remove(tilausrivit);
                db.SaveChanges();
                return RedirectToAction("Index");
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
