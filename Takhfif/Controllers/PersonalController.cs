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
    [Secure(UserType = "*", Admin = true)]    
    public class PersonalController : Controller
    {
        private ModelContainer db = new ModelContainer();


        public ActionResult Index()
        {
            string Username = Session["Username"].ToString();
            var posts = db.Posts.Where(m=>m.AdminUsername == Username).OrderByDescending(m => m.Id).Take(10);
            return View(posts.ToList());
        }



        public ActionResult Details(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(post);
            return View(post);
        }

        
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }



        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            post.AdminUsername = Session["Username"].ToString();
            post.Date = About_Controller.CurrentDate;
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(post);
            return View(post);
        }



        public ActionResult Edit(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post.AdminUsername.ToString() != Session["Username"].ToString())
            {
                return HttpNotFound();
            }
            if (post == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(post);
            return View(post);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            post.Date = About_Controller.CurrentDate;
            post.AdminUsername = Session["Username"].ToString();
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminUsername = new SelectList(db.Admins, "Username", "Type", post.AdminUsername);
            if (Request.IsAjaxRequest())
                return PartialView(post);
            return View(post);
        }


        public ActionResult Delete(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post.AdminUsername.ToString() != Session["Username"].ToString())
            {
                return HttpNotFound();
            }
            if (post == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(post);
            return View(post);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            if (post.AdminUsername.ToString() != Session["Username"].ToString())
            {
                return HttpNotFound();
            }
            db.Posts.Remove(post);
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