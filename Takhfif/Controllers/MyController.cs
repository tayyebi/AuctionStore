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
    [Secure(UserType = "*")]
    public class MyController : Controller
    {
        ModelContainer db = new ModelContainer();
        public ActionResult Index()
        {
            return Redirect("~/My/Bank");
        }

        public ActionResult Profile()
        {
            string Username = Session["Username"].ToString();
            return View(db.Users.Find(Username));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Profile(User user)
        {
            user.Username = Session["Username"].ToString();
            User _user = db.Users.Find(user.Username);
            if (_user == null)
                return HttpNotFound();
            user.Bank = _user.Bank;
            user.IsBlocked = _user.IsBlocked;
            ((IObjectContextAdapter)db).ObjectContext.Detach(_user);

            if (user.Birth_Month != null)
            {
                if (user.Birth_Month > 12)
                {
                    ViewBag.Message = "لطفا در وارد کردن ماه تولد دقت نمائید.";
                    return View(user);
                }
                else if (user.Birth_Month < 1)
                {
                    ViewBag.Message = "لطفا در وارد کردن ماه تولد دقت نمائید.";
                    return View(user);
                }
            }
            if (user.Birth_Year != null && user.Birth_Year < 1300)
            {
                ViewBag.Message = "لطفا در وارد کردن سال تولد دقت نمائید.";
                return View(user);
            }
            if (user.Rate != null)
            {
                if (user.Rate > 100)
                {
                    ViewBag.Message = "امتیاز نمیتواند بیشتر از 100 باشد.";
                    return View(user);
                }
                else if (user.Rate < 0)
                {
                    ViewBag.Message = "امتیاز نمیتواند از 0 کمتر باشد.";
                    return View(user);
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(user);
        }
        public ActionResult Bank(int Id = 10)
        {
            string Username = Session["Username"].ToString();
            string Credit = db.Users.Find(Username).Bank.ToString();
            ViewBag.Credit = Credit;
            var transactions = db.Transactions.OrderByDescending(m => m.Id).Take(Id);
            if (Request.IsAjaxRequest())
                return PartialView(transactions.ToList());
            return View(transactions.ToList());
        }

        public ActionResult Transfer(int Id = 50)
        {
            string Username = Session["Username"].ToString();
            var transfers = db.Transfers.OrderByDescending(m => m.OrderId).Where(m => m.UserUsername == Username).Take(Id);
            if (Request.IsAjaxRequest())
                return PartialView(transfers);
            return View(transfers);
        }

        public ActionResult Offline()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Offline(Transfer transfer)
        {
            transfer.UserUsername = Session["Username"].ToString();
            if (transfer.Value < 1)
            {
                ViewBag.Message = "لطفا در وارد کردن مقدار واریز دقت نمائید.";
            }
            if (transfer.Date_Month > 12)
            {
                ViewBag.Message = "لطفا در وارد کردن ماه واریز دقت نمائید.";
                return View(transfer);
            }
            else if (transfer.Date_Month < 1)
            {
                ViewBag.Message = "لطفا در وارد کردن ماه واریز دقت نمائید.";
                return View(transfer);
            }

            if (transfer.Date_Year < 1300)
            {
                ViewBag.Message = "لطفا در وارد کردن سال واریز دقت نمائید.";
                return View(transfer);
            }

            if (transfer.Date_Day > 31)
            {
                ViewBag.Message = "لطفا در وارد کردن روز واریز دقت نمائید.";
                return View(transfer);
            }
            else if (transfer.Date_Day < 1)
            {
                ViewBag.Message = "لطفا در وارد کردن روز واریز دقت نمائید.";
                return View(transfer);
            }

            transfer.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                db.Transfers.Add(transfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "خطا در ارسال درخواست.";
            if (Request.IsAjaxRequest())
                return PartialView(transfer);
            return View(transfer);
        }
        public ActionResult Basket()
        {
            string Username = Session["Username"].ToString();
            var orders = db.Orders.Where(m => m.UserUsername == Username).OrderByDescending(m => m.Id).ToList();
            if (Request.IsAjaxRequest())
                return PartialView(orders);
            return View(orders);
        }
        public ActionResult Finalization(int id)
        {
            Order order = db.Orders.Find(id);
            if (!order.Status.StartsWith("1")) return HttpNotFound();
            if (order == null)
                return HttpNotFound();
            if (order.UserUsername != Session["Username"].ToString())
                return HttpNotFound();
            ViewBag.Order = order.Product.Name;
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finalization(int Id, int OrderId)
        {
            if (Id != OrderId) return HttpNotFound();

            string Username = Session["Username"].ToString();

            Order order = db.Orders.Find(OrderId);
            if (order == null) return HttpNotFound();

            if (!order.Status.StartsWith("1")) return HttpNotFound();
            ViewBag.Order = order.Product.Name;

            if (order.UserUsername != Username)
                return HttpNotFound();

            User user = db.Users.Find(Username);

            if (user.Bank < order.Price)
            {
                ViewBag.Message = "موجودی حساب شما کافی نیست.";
                return View();
            }

            int[] _Now = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss").Replace(" ", null).Replace(":", null).Replace("-", null).Replace("/", null).Select(c => int.Parse(c.ToString())).ToArray();
            int[] _Expire = order.Date_Expire.Replace(" ", null).Replace(":", null).Replace("-", null).Replace("/", null).Select(c => int.Parse(c.ToString())).ToArray();
            int i = 0;
            foreach (var item in _Now)
            {
                if (_Now[i] == _Expire[i])
                    i++;
                else if (_Now[i] > _Expire[i])
                {
                    ViewBag.Message = "زمان خرید به پایان رسیده است.";
                    return View();
                }
            }

            Transaction _transaction = new Transaction();
            _transaction.Direction = true;
            _transaction.Date = About_Controller.CurrentDate;
            _transaction.UserUsername = order.UserUsername;
            _transaction.Value = order.Price * -1;
            db.Transactions.Add(_transaction);
            db.SaveChanges();

            user.Bank = user.Bank - order.Price;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            ((IObjectContextAdapter)db).ObjectContext.Detach(user);

            order.Status = "2- در انتظار تائید ادمین";

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Basket");

            ViewBag.Message = "فرایند با شکست مواجه شد.";
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        public ActionResult Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (!order.Status.StartsWith("1") && !order.Status.StartsWith("4")) return HttpNotFound();
            if (order == null)
                return HttpNotFound();
            if (order.UserUsername != Session["Username"].ToString())
                return HttpNotFound();
            ViewBag.Order = order.Product.Name;
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id, int OrderId)
        {
            if (Id != OrderId) return HttpNotFound();

            string Username = Session["Username"].ToString();

            Order order = db.Orders.Find(OrderId);
            if (order == null) return HttpNotFound();

            if (!order.Status.StartsWith("1") && !order.Status.StartsWith("4")) return HttpNotFound();
            ViewBag.Order = order.Product.Name;

            if (order.UserUsername != Username)
                return HttpNotFound();

            db.Orders.Remove(order);
            db.SaveChanges();

            return RedirectToAction("Basket");

        }
    }
}