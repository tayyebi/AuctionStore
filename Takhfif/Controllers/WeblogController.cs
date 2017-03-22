using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    public class WeblogController : Controller
    {
        ModelContainer db = new ModelContainer();
        public ActionResult Index(string Username, string Id)
        {
            if (String.IsNullOrEmpty(Username))
            {
                // Go to admins list
            }
            else
            {
                try
                {
                    string _Usrename = db.Admins.Find(Username).Username;
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            return View();
        }
        public ActionResult Post(int Id)
        {
            try
            {
                return View();
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}
