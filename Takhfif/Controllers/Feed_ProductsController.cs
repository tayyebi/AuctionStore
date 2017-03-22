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
    public class Feed_ProductsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Product_Archive(int Index = 0, int Size = 50)
        {
            return View(db.Products.OrderByDescending(m => m.Id).Skip(Index * Size).Take(Size).ToList());
        }
        public ActionResult Product_Search(string Id)
        {
            if (Id == null) return HttpNotFound();
            return View(db.Products.Where(m => m.Name.Contains(Id)).OrderByDescending(m => m.Id).Take(50));
        }
        public ActionResult Product_Last(int Id = 30)
        {
            return View(db.Products.Where(m => m.IsActive == true).Take(Id).ToList());
        }

        public ActionResult Product_Priority(int Id = 10)
        {
            return View(db.Products.OrderByDescending(m => m.Id).OrderByDescending(m => m.Priority).Take(Id).ToList());
        }

        public ActionResult Product_Buy(int Id = 10)
        {
            return View(db.Products.OrderByDescending(m => m.Id).OrderByDescending(m => m.Count_Buy).Take(Id).ToList());
        }

        public ActionResult Product_Like(int Id = 10)
        {
            return View(db.Products.OrderByDescending(m => m.Id).OrderByDescending(m => m.Count_Like).Take(Id).ToList());
        }

        public ActionResult Cities()
        {
            return View(db.Cities.ToList());

        }
        public ActionResult City(Guid? id = null)
        {
            return View(db.Products.Where(m => m.CityId == id).ToList());
        }


        public ActionResult Groups(string Id)
        {
            return View(db.Groups.ToList());
        }
        public ActionResult Group(Guid? id = null)
        {
            return View(db.Products.Where(m => m.GroupId == id).ToList());
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}