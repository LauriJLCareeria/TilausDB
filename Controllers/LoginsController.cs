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
    public class LoginsController : Controller
    {
        private TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Logins
        public ActionResult Index()
        {
            if (Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            //else if (Session["KäyttäjätunnusAdmin"] == null)
            //{
            //    return RedirectToAction("AccessDenied", "Logins");
            //}
            else
            {
                return View(db.Logins.ToList());
            }
        }

        // GET: Logins/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Logins logins = db.Logins.Find(id);
                if (logins == null)
                {
                    return HttpNotFound();
                }
                return View(logins);
            }
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            if (Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                List<SelectListItem> userrolelista = new List<SelectListItem>();
                foreach (UserRoles userrole in db.UserRoles)
                {
                    userrolelista.Add(new SelectListItem
                    {
                        Value = userrole.Rooli.ToString(),
                        Text = userrole.Rooli.ToString()
                    });
                }

                ViewBag.Rooli = new SelectList(userrolelista, "Value", "Text", null);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginId,Käyttäjätunnus,Salasana,Rooli")] Logins logins)
        {
            if (Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Logins.Add(logins);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(logins);
            }
        }

        // GET: Logins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<SelectListItem> userrolelista = new List<SelectListItem>();
                foreach (UserRoles userrole in db.UserRoles)
                {
                    userrolelista.Add(new SelectListItem
                    {
                        Value = userrole.Rooli,
                        Text = userrole.Rooli.ToString()
                    });
                }

                ViewBag.Rooli = new SelectList(userrolelista, "Value", "Text", null);
                Logins logins = db.Logins.Find(id);
                if (logins == null)
                {
                    return HttpNotFound();
                }
                return View(logins);
            }
        }

        // POST: Logins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginId,Käyttäjätunnus,Salasana,Rooli")] Logins logins)
        {
            if (Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(logins).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(logins);
            }
        }

        // GET: Logins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Logins logins = db.Logins.Find(id);
                if (logins == null)
                {
                    return HttpNotFound();
                }
                return View(logins);
            } 
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["KäyttäjätunnusSuper"] == null)
            {
                return RedirectToAction("AccessDenied", "Logins");
            }
            else
            {
                Logins logins = db.Logins.Find(id);
                db.Logins.Remove(logins);
                db.SaveChanges();
                return RedirectToAction("Index");
            }             
        }

        public ActionResult AccessDenied()
        {
            return View();
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
