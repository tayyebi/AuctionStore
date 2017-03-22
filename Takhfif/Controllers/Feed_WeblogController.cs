using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    public class Feed_WeblogController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Admins()
        {
            return View(db.Admins.ToList());
        }

        public ActionResult Admins_Profile(string Id)
        {
            try
            {
                Admin admin = db.Admins.Find(Id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            catch { return HttpNotFound(); }
        }


        public ActionResult LastPost_Police()
        {
            Post post = db.Posts.Where(m => m.Admin.Type == "Police").OrderByDescending(m => m.Id).FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        public ActionResult LastPost_Admin()
        {
            Post post = db.Posts.Where(m => m.Admin.Type != "Police").OrderByDescending(m => m.Id).FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            } 
            return View(post);
        }

        public ActionResult Top()
        {
            return View(db.Posts.OrderByDescending(m => m.Id).Take(10).ToList());
        }

        public ActionResult Top_Username(string Id)
        {
            if (Id == null) return HttpNotFound();
            return View(db.Posts.Where(m => m.AdminUsername == Id).OrderByDescending(m => m.Id).Take(10).ToList());
        }

        public ActionResult Post(int Id)
        {
            try
            {
                Post post = db.Posts.Find(Id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(post);
            }
            catch { return HttpNotFound(); }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}