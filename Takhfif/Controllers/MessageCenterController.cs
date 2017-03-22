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
    [Secure(UserType="*")]
    public class MessageCenterController : Controller
    {
        private ModelContainer db = new ModelContainer();

        //
        // GET: /MessageCenter/

        public ActionResult Index(int Id = 10)
        {
            string Username =
                Session["Username"].ToString();
            return View(db.InMails.Where(m => m.IsRead == false).Where(m => m.ToUsername == Username).Take(Id).OrderByDescending(m => m.Id).ToList());
        }

        public ActionResult Archive(int Id = 50)
        {
            string Username =
                Session["Username"].ToString();
            if (Request.IsAjaxRequest())
                return PartialView(db.InMails.OrderByDescending(m => m.Id).Where(m => m.ToUsername == Username).Take(Id).ToList());
            return View(db.InMails.OrderByDescending(m => m.Id).Where(m => m.ToUsername == Username).Take(Id).ToList());
        }

        public ActionResult Compose(string Id = null)
        {
            if (Id == null)
            {
                if (Request.IsAjaxRequest())
                    return PartialView();
                return View();
            }
            else
            {
                InMail inmail = new InMail();
                inmail.ToUsername = Id;
                if (Request.IsAjaxRequest())
                    return PartialView(inmail);
                return View(inmail);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Compose(InMail inmail)
        {
            inmail.IsRead = false;
            inmail.Date = About_Controller.CurrentDate;
            inmail.FromUsername = Session["Username"].ToString();
            if (inmail.ToUsername != inmail.FromUsername)
            {
                if (ModelState.IsValid)
                {
                    db.InMails.Add(inmail);
                    db.SaveChanges();
                    return Redirect("~/MessageCenter/Index");
                }
            }
            else
            {
                ViewBag.Message = "خطا! ارسال با خطا رو به رو شد. لطفا در وارد کردن اطلاعات دقت نمائید.";
            }
            if (Request.IsAjaxRequest())
                return PartialView(inmail);
            return View(inmail);

        }

        public ActionResult Read(int Id)
        {
            InMail inmail = db.InMails.Find(Id);
            if (inmail == null)
                return HttpNotFound();
            
            string Username = inmail.ToUsername;
            if (Username == Session["Username"].ToString())
            {
                try
                {
                    string Type = db.Admins.Find(inmail.FromUsername).Type;
                    if (Type != null)
                    {
                        ViewBag.Admin = "[" + Username + "-" + Type + "]";
                    }
                }
                catch { }
                if (inmail.IsRead == false)
                {
                    inmail.IsRead = true;
                    db.Entry(inmail).State = EntityState.Modified;
                    db.SaveChanges();
                }

                if (Request.IsAjaxRequest())
                    return PartialView(inmail);
                return View(inmail);
            }
            else return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}