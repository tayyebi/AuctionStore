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
    [Secure(Admin=true,UserType="*")]
    public partial class UploadController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Archive()
        {
            string Username = Session["Username"].ToString();
            var uploadset = db.Uploads.Where(m => m.AdminUsername == Username).OrderByDescending(m => m.Date);
            return View(uploadset.ToList());
        }


        public ActionResult Index()
        {
            string Username = Session["Username"].ToString();
            var uploadset = db.Uploads.OrderByDescending(m => m.Date).Where(m => m.AdminUsername == Username).Take(10);
            return View(uploadset);
        }

        public ActionResult Details(Guid? id = null)
        {
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(upload);
            return View(upload);

        }

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Upload upload, HttpPostedFileBase FileUploader)
        {
            upload.Lenght = FileUploader.ContentLength;
            upload.Type = FileUploader.ContentType;
            upload.Date = About_Controller.CurrentDate;
            upload.Id = Guid.NewGuid();
            upload.AdminUsername = Session["Username"].ToString();

            using (MemoryStream ms = new MemoryStream())
            {
                FileUploader.InputStream.CopyTo(ms);
                upload.Bytes = ms.GetBuffer();
            }
            if (ModelState.IsValid)
            {
                db.Uploads.Add(upload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
                return PartialView(upload);
            return View(upload);
        }



        public ActionResult Delete(Guid? id = null)
        {
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            else if (upload.AdminUsername == Session["Username"].ToString())
            {
                return HttpNotFound();
            }
            else
            {
                if (Request.IsAjaxRequest())
                    return PartialView(upload);
                return View(upload);
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Upload upload = db.Uploads.Find(id);
            if (upload.AdminUsername == Session["Username"].ToString())
            {
                return HttpNotFound();
            }
            else
            {
                db.Uploads.Remove(upload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}