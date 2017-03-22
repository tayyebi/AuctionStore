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
    [Secure(Admin=true,UserType="*")]
    // UserType = Vendor
    public class Product_ImageController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Index(int? Id = null)
        {
            if (Id != null)
            {
                var images = db.Images.Where(m => m.ProductId == Id).OrderByDescending(m => m.Id);
                if (images == null) return HttpNotFound();
                else
                {
                    if (Request.IsAjaxRequest())
                        return PartialView(images.ToList());
                    else return View(images.ToList());
                }
            }
            else
            {
                var images = db.Images.OrderByDescending(m => m.Id).Take(100).ToList();
                if (Request.IsAjaxRequest())
                    return PartialView(images.ToList());
                return View(images.ToList());
            }
        }

        public ActionResult Details(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
                return HttpNotFound();
            return View(image);
        }

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(image);
            return View(image);
        }

        public ActionResult Edit(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }

            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(image);
        }

        public ActionResult Delete(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
                return HttpNotFound();
            return View(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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