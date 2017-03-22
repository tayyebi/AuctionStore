using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    public class HomeController : Controller
    {
        private ModelContainer db = new ModelContainer();

        [ChildActionOnly]
        public PartialViewResult Social_Follow()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult Social_Share()
        {
            return PartialView();
        }
        /*-------------------------------------------------------*/
        [ChildActionOnly]
        public PartialViewResult Slider()
        {
            var slides = db.Slides.OrderByDescending(m => m.Id).Take(3);
            return PartialView(slides.ToList());
        }
        [ChildActionOnly]
        public PartialViewResult Group()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult City()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult Admin()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult Police()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult RssList()
        {
            return PartialView();
        }
        /*-------------------------------------------------------*/
        public ActionResult Index()
        {
            return View();
        }

    }
}
