using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.App_Start;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    [Secure(UserType = "Head", Admin = true)]
    public class HeadController : Controller
    {
        private ModelContainer db = new ModelContainer();

        //
        // GET: /Head/

        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        //
        // GET: /Head/Details/5

        public ActionResult Details(string id = null)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(admin);
            return View(admin);
        }

        //
        // GET: /Head/Create

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        //
        // POST: /Head/Create

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(admin);
            return View(admin);
        }

        //
        // GET: /Head/Edit/5

        public ActionResult Edit(string id = null)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(admin);
            return View(admin);
        }

        //
        // POST: /Head/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admin admin, string id)
        {
            admin.Username = id;
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            if (Request.IsAjaxRequest())
                return PartialView(admin);
            return View(admin);
        }

        //
        // GET: /Head/Delete/5

        public ActionResult Delete(string id = null)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
                return PartialView(admin);
            return View(admin);
        }

        //
        // POST: /Head/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Admin admin = db.Admins.Find(id);
            if (admin.Username.ToLower() != Session["Username"].ToString().ToLower())
            {
                db.Admins.Remove(admin);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Information()
        {
            if (!Directory.Exists(Server.MapPath("~/App_About")))
            {
                Directory.CreateDirectory(Server.MapPath("~/App_About"));
            }
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Information(string Part, string Value)
        {
            System.IO.File.WriteAllText(Server.MapPath("~/App_About/" + Part + ".html"), Value);
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}