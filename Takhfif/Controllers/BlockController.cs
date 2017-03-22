using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.Models;
using Takhfif.App_Start;
using System.Data;

namespace Takhfif.Controllers
{
    [Secure(Admin = true, UserType = "Police")]
    public class BlockController : Controller
    {
        ModelContainer db = new ModelContainer();
        public ActionResult List()
        {
            var users = db.Users.Where(m => m.IsBlocked == true);
            if (Request.IsAjaxRequest())
                return PartialView(users.ToList());
            return View(users.ToList());
        }
        public ActionResult Index(string Id = null)
        {
            ViewBag.Username = Id;
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Username, bool IsBlocked)
        {
            User user = db.Users.Find(Username);
            if (user == null) return HttpNotFound();
            user.IsBlocked = IsBlocked;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            string MessageText = "شما از لیست سیاه پلیس وب سایت خارج شدید.";
            if (IsBlocked) 
                MessageText = "شما در لیست سیاه پلیس قرار گرفته اید.";
            _Admins.SendMessage(Session["Username"].ToString(), Username, MessageText);
            if (Request.IsAjaxRequest())
                return PartialView(user);
            return View(user);
        }

    }
}
