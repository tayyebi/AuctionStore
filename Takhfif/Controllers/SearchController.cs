using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Takhfif.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index(string id)
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else return View();
        }
        public PartialViewResult Component()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Component(string Search_Value = null)
        {
            if (Search_Value == null) return PartialView();
            Search_Value = Regex.Replace(Search_Value, @"[^0-9a-zA-Zابپتثجچحخدذرزژسشصضظطعغفقکگلمنوهی آئ]+", "");
            HttpContext.Response.Redirect("~/Search/Index/" + Search_Value);
            return null;
        }
    }
}
