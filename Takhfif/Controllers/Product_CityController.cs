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
    public class Product_cityController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView(db.Cities.ToList());
            return View(db.Cities.ToList());
        }

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                city.Id = Guid.NewGuid();
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView(city);
            return View(city);
        }



        public ActionResult Edit(Guid? id = null)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(city);
            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView(city);
            return View(city);
        }

        public ActionResult Delete(Guid? id = null)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(city);
            return View(city);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
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