using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.App_Start;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    [Secure(UserType = "Head", Admin = true)]
    public class Product_GroupController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView(db.Groups.ToList());
            return View(db.Groups.ToList());
        }

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                group.Id = Guid.NewGuid();
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView(group);
            return View(group);
        }



        public ActionResult Edit(Guid? id = null)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(group);
            return View(group);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView(group);
            return View(group);
        }

        public ActionResult Delete(Guid? id = null)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(group);
            return View(group);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}