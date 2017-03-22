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
    public class CardController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Comment(int Id)
        {
            return PartialView(db.Comments.Where(m => m.ProductId == Id).ToList());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Comment(int Id, string value, string username)
        {
            try
            {
                if (username != Session["Username"].ToString())
                    return null;
                if (db.Users.Find(username).IsBlocked == true)
                    return null;
            }
            catch { return null; }
            Comment comment = new Comment();
            comment.UserUsername = username;
            comment.Value = System.Text.RegularExpressions.Regex.Replace(value, @"<[^>]*>", "");
            comment.Date = About_Controller.CurrentDate;
            comment.ProductId = Id;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            return PartialView(db.Comments.Where(m => m.ProductId == Id).ToList());
        }
        public ActionResult View(int Id)
        {
            Product product = db.Products.Find(Id);
            if (product == null)
                return HttpNotFound();
            if (product.IsActive == false)
                return HttpNotFound();

            int[] _Now = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss").Replace(" ", null).Replace(":", null).Replace("-", null).Replace("/", null).Select(c => int.Parse(c.ToString())).ToArray();
            int[] _Expire = product.Date_Expire.Replace(" ", null).Replace(":", null).Replace("-", null).Replace("/", null).Select(c => int.Parse(c.ToString())).ToArray();
            int i = 0;
            foreach (var item in _Now)
            {
                if (_Now[i] == _Expire[i])
                    i++;
                else if (_Now[i] > _Expire[i])
                    return HttpNotFound();
            }
            return PartialView(product);
        }


        public FileResult Thumbnail(int Id)
        {
            try
            {
                return File(db.Products.Find(Id).Thumbnail, "image");
            }
            catch
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}