using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Takhfif.Controllers
{
    public class InformationController : Controller
    {
        public ActionResult Index(string id)
        {
            try
            {
                Server.Transfer("~/App_About/" + id + ".html");
            }
            catch
            {
                return HttpNotFound();
            }
            return null;
        }

    }
}
