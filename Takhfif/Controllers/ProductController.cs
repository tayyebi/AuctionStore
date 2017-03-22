using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.App_Start;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    [Secure(Admin=true,UserType="*")]
    // UserType= Vendor
    public class ProductController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Index(int Id = 50)
        {
            // Id = Take
            var products = db.Products.OrderByDescending(m => m.Id).Take(Id);
            return View(products.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(product);
            return View(product);
        }


        public ActionResult Create()
        {
            IEnumerable<SelectListItem> CityId = new SelectList(db.Cities.ToList(), "id", "Name");
            ViewData["CityId"] = CityId;
            IEnumerable<SelectListItem> GroupId = new SelectList(db.Groups.ToList(), "id", "Name");
            ViewData["GroupId"] = GroupId;

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase FileUploader, int expire_day = 0, int expire_hour = 0)
        {
            IEnumerable<SelectListItem> CityId = new SelectList(db.Cities.ToList(), "id", "Name");
            ViewData["CityId"] = CityId;
            IEnumerable<SelectListItem> GroupId = new SelectList(db.Groups.ToList(), "id", "Name");
            ViewData["GroupId"] = GroupId;

            if (product.Price_Off_Percent < 0)
            {
                ViewBag.Message = "لطفا مقدار تخفیف را درست وارد نمائید";
                return View();
            }
            if (product.Price_Off_Percent> 100)
            {
                ViewBag.Message = "لطفا مقدار تخفیف را درست وارد نمائید";
                return View();
            }

            if (FileUploader == null)
            {
                ViewBag.Message = "لطفا یک تصویر انتخاب نمائید.";
                return View();
            }
            else if (!FileUploader.ContentType.StartsWith("image"))
            {
                ViewBag.Message = "این فایل یک تصویر معتبر نیست.";
                return View();
            }
            product.Date_Create = About_Controller.CurrentDate;
            product.Date_Expire=About_Controller.Date(0,0,expire_day,expire_hour);
            int price_Off = product.Price_Original / 100 * product.Price_Off_Percent;
            product.Price_Off = product.Price_Original - price_Off;
            product.Count_Buy = 0;
            product.Count_Like = 0;
            product.AdminUsername = Session["Username"].ToString();

            var _Bytes = new byte[FileUploader.ContentLength];
            FileUploader.InputStream.Read(_Bytes, 0, FileUploader.ContentLength);
            System.Drawing.Image _Image;
            using (var ms = new MemoryStream(_Bytes))
            {
                _Image = System.Drawing.Image.FromStream(ms);
            }
            product.Thumbnail = Thumbnail.ConvertImageToByte(Thumbnail.ResizeImage(_Image, new Size(400, 300)));

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }


        public ActionResult Edit(int id = 0)
        {
            IEnumerable<SelectListItem> CityId = new SelectList(db.Cities.ToList(), "id", "Name");
            ViewData["CityId"] = CityId;
            IEnumerable<SelectListItem> GroupId = new SelectList(db.Groups.ToList(), "id", "Name");
            ViewData["GroupId"] = GroupId;


            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int Id,Product product, HttpPostedFileBase FileUploader = null, int expire_day = 0, int expire_hour = 0)
        {
            IEnumerable<SelectListItem> CityId = new SelectList(db.Cities.ToList(), "id", "Name");
            ViewData["CityId"] = CityId;
            IEnumerable<SelectListItem> GroupId = new SelectList(db.Groups.ToList(), "id", "Name");
            ViewData["GroupId"] = GroupId;

            
            if (product.Price_Off_Percent < 0)
            {
                ViewBag.Message = "لطفا مقدار تخفیف را درست وارد نمائید";
                return View();
            }
            if (product.Price_Off_Percent > 100)
            {
                ViewBag.Message = "لطفا مقدار تخفیف را درست وارد نمائید";
                return View();
            }

            if (FileUploader != null)
            {
                if (FileUploader.ContentType.StartsWith("image"))
                {
                    var _Bytes = new byte[FileUploader.ContentLength];
                    FileUploader.InputStream.Read(_Bytes, 0, FileUploader.ContentLength);
                    System.Drawing.Image _Image;
                    using (var ms = new MemoryStream(_Bytes))
                    {
                        _Image = System.Drawing.Image.FromStream(ms);
                    }
                    product.Thumbnail = Thumbnail.ConvertImageToByte(Thumbnail.ResizeImage(_Image, new Size(400, 300)));
                }
            }
            else if (FileUploader == null)
            {
                Product exiting = db.Products.Find(Id);
                product.Thumbnail = exiting.Thumbnail;
                ((IObjectContextAdapter)db).ObjectContext.Detach(exiting);
            }

            product.Date_Create = About_Controller.CurrentDate;
            product.Date_Expire = About_Controller.Date(0, 0, expire_day, expire_hour);
            int price_Off = product.Price_Original / 100 * product.Price_Off_Percent;
            product.Price_Off = product.Price_Original - price_Off;
            product.Count_Buy = 0;
            product.Count_Like = 0;
            product.AdminUsername = Session["Username"].ToString();

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }


        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
                return PartialView(product);
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}