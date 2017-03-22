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
    public class SliderController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Index()
        {
            var slides = db.Slides.OrderByDescending(m => m.Id).Take(3);
            if (Request.IsAjaxRequest())
                return PartialView(slides.ToList());
            return View(slides.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(slide);
            return View(slide);
        }


        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slide slide)
        {
            slide.AdminUsername = Session["Username"].ToString();
            slide.Date = About_Controller.CurrentDate;
            if (ModelState.IsValid)
            {
                db.Slides.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(slide);
            return View(slide);
        }


        public ActionResult Edit(int id = 0)
        {
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(slide);
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slide slide)
        {
            slide.Date = About_Controller.CurrentDate;
            slide.AdminUsername = Session["Username"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return PartialView(slide);
            return View(slide);
        }

        public ActionResult Delete(int id = 0)
        {
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(slide);
            return View(slide);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slides.Find(id);
            db.Slides.Remove(slide);
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