using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.App_Start;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    public class StoreController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult Slider(int Id)
        {
            var slides = db.Images.Where(m => m.ProductId == Id).OrderByDescending(m => m.Id);
            return PartialView(slides.ToList());
        }
        ModelContainer db = new ModelContainer();
        public ActionResult Product(int Id)
        {
            Product product = db.Products.Find(Id);
            if (product == null) return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(product);
            return View(product);
        }
        public ActionResult City(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        public ActionResult Group(Guid? Id = null)
        {
            if (Id == null) return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        public ActionResult Search(string Id)
        {
            ViewBag.Search = Id;
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [Secure(UserType = "*")]
        public ActionResult Like(int Id)
        {
            string Username = Session["Username"].ToString();
            Like _like = db.Likes.Where(m => m.UserUsername == Username && m.ProductId == Id).FirstOrDefault();
            if (_like != null)
            {
                db.Likes.Remove(_like);
                db.SaveChanges();
            }
            ((IObjectContextAdapter)db).ObjectContext.Detach(_like);

            Like like = new Like();
            like.ProductId = Id;
            like.UserUsername = Username;
            db.Likes.Add(like);
            db.SaveChanges();
            ((IObjectContextAdapter)db).ObjectContext.Detach(like);

            Product product = db.Products.Find(Id);
            int Likes = product.Count_Like;
            Likes++;
            product.Count_Like = Likes;
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        [Secure(UserType = "*")]
        public ActionResult Buy(int Id)
        {
            string Username = Session["Username"].ToString();
            
            User user = db.Users.Find(Username);
            Product product = db.Products.Find(Id);
            if (user.Bank < product.Price_Off)
            {
                ViewBag.Message = "موجودی حساب شما کافی نیست.";
                if (Request.IsAjaxRequest())
                    return PartialView();
                return View();
            }

            Order order = new Order();
            order.Date_Buy = About_Controller.CurrentDate;
            order.Date_Expire = product.Date_Expire;
            order.ProductId = product.Id;
            order.Status = "1- در انتظار تائید کاربر";
            order.UserUsername = Username;
            order.Price = product.Price_Off;
            db.Orders.Add(order);
            db.SaveChanges();
            ((IObjectContextAdapter)db).ObjectContext.Detach(order);

            int Buys = product.Count_Buy;
            Buys++;
            product.Count_Buy = Buys;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();


            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult Archive(int Index = 0, int Size = 100)
        {
            ViewBag.Index = Index.ToString();
            ViewBag.Size = Size.ToString();
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

    }
}