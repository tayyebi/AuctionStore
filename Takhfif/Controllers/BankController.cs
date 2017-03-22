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
    [Secure(Admin = true, UserType = "*")]
    // UserType = Accountant
    public class BankController : Controller
    {
        ModelContainer db = new ModelContainer();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Offline(int Id = 50)
        {
            var transfers = db.Transfers.Where(m => m.IsConfirmed == false).Take(Id).OrderBy(m => m.OrderId);
            if (Request.IsAjaxRequest())
                return PartialView(transfers);
            return View(transfers);
        }

        public ActionResult Transfer(Guid Id)
        {
            var transfer = db.Transfers.Find(Id);
            if (Request.IsAjaxRequest())
                return PartialView(transfer);
            return View(transfer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(Transfer transfer)
        {
            string AdminUsername = Session["Username"].ToString();
            if (transfer.IsConfirmed == true)
            {
                try
                {
                    Transfer _transfer = db.Transfers.Find(transfer.Id);
                    transfer.BranchCode = _transfer.BranchCode;
                    transfer.Date_Day = _transfer.Date_Day;
                    transfer.Date_Month = _transfer.Date_Month;
                    transfer.Date_Year = _transfer.Date_Year;
                    transfer.OrderId = _transfer.OrderId;
                    transfer.Refer = _transfer.Refer;
                    transfer.User = _transfer.User;
                    transfer.UserUsername = _transfer.UserUsername;
                    transfer.Value = _transfer.Value;

                    ((IObjectContextAdapter)db).ObjectContext.Detach(_transfer);

                    db.Entry(transfer).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch
                {
                    _Admins.SendMessage(AdminUsername, transfer.UserUsername, "سیستم در مرحله 'معتبر شناختن واریز کاربر' به لیست کاربر دچار مشکل شد. اگر تا 2 ساعت آینده این فرایند تکمیل نشد با ادمین های مالی تماس بگیرید.");
                    _Admins.SendMessage(AdminUsername, AdminUsername, "سیستم در مرحله 'معتبر شناختن واریز کاربر' به لیست کاربر دچار مشکل شد. کاربر " + transfer.UserUsername + " پس از 2 ساعت با شما تماس خواهد گرفت.");
                }

                try
                {
                    // Add transfer to transaction
                    Transaction _transaction = new Transaction();
                    _transaction.Direction = true;
                    _transaction.Date = About_Controller.CurrentDate;
                    _transaction.UserUsername = transfer.UserUsername;
                    _transaction.Value = transfer.Value;
                    db.Transactions.Add(_transaction);
                    db.SaveChanges();

                    ((IObjectContextAdapter)db).ObjectContext.Detach(_transaction);
                }
                catch
                {
                    _Admins.SendMessage(AdminUsername, transfer.UserUsername, "سیستم در مرحله افزودن 'واریز کارت به کارت' به لیست کاربر دچار مشکل شد. اگر تا 2 ساعت آینده این فرایند تکمیل نشد با ادمین های مالی تماس بگیرید.");
                    _Admins.SendMessage(AdminUsername, AdminUsername, "سیستم در مرحله افزودن 'واریز کارت به کارت' به لیست کاربر دچار مشکل شد. کاربر " + transfer.UserUsername + " پس از 2 ساعت با شما تماس خواهد گرفت.");
                }
                try
                {
                    // Update users bank
                    User _user = db.Users.Find(transfer.UserUsername);
                    if (_user.Bank == null) _user.Bank = 0;
                    _user.Bank = _user.Bank + transfer.Value;

                    ((IObjectContextAdapter)db).ObjectContext.Detach(transfer);

                    db.Entry(_user).State = EntityState.Modified;
                    db.SaveChanges();

                    ((IObjectContextAdapter)db).ObjectContext.Detach(_user);

                    _Admins.SendMessage(AdminUsername, transfer.UserUsername, "واریز شما به مبلغ " + transfer.Value + "به ارجاع" + transfer.Refer + " در سیستم ثبت گردید.");
                }
                catch
                {
                    _Admins.SendMessage(AdminUsername, transfer.UserUsername, "سیستم در مرحله 'به روز رسانی حساب کاربر' به لیست کاربر دچار مشکل شد. اگر تا 2 ساعت آینده این فرایند تکمیل نشد با ادمین های مالی تماس بگیرید.");
                    _Admins.SendMessage(AdminUsername, AdminUsername, "سیستم در مرحله 'به روز رسانی حساب کاربر' به لیست کاربر دچار مشکل شد. کاربر " + transfer.UserUsername + " پس از 2 ساعت با شما تماس خواهد گرفت.");
                }

                return RedirectToAction("Offline");

            }
            else if (transfer.IsConfirmed == false)
            {
                // Delete Transfer from database
                Transfer _transfer = db.Transfers.Find(transfer.Id);
                db.Transfers.Remove(_transfer);
                db.SaveChanges();

                _Admins.SendMessage(AdminUsername, transfer.UserUsername, "واریز شما معتبر تشخیص داده نشده و حذف شد. لطفا اطلاعات را دوباره ارسال نموده و یا با ادمین های مالی تماس بگیرید.");
                _Admins.SendMessage(AdminUsername, AdminUsername, "واریز کاربر " + transfer.UserUsername + " معتبر شناخته نشد. منتظر تماس از این کاربر باشید.");

                return RedirectToAction("Offline");
            }
            else return HttpNotFound();
        }

        public ActionResult Finalization_Archive(int Id = 50)
        {
            var orders = db.Orders.Where(m => m.Status.StartsWith("1") == false/*&& m.Status.StartsWith("4") == false*/).Take(Id).OrderBy(m => m.Id);
            if (Request.IsAjaxRequest())
                return PartialView(orders);
            return View(orders);
        }
        public ActionResult Finalization_Print()
        {
            var orders = db.Orders.Where(m => m.Status.StartsWith("3")).OrderBy(m => m.Id);
            if (Request.IsAjaxRequest())
                return PartialView(orders);
            return View(orders);
        }
        public ActionResult Finalization(int Id = 50)
        {
            var orders = db.Orders.Where(m => m.Status.StartsWith("2")).Take(Id).OrderBy(m => m.Id);
            if (Request.IsAjaxRequest())
                return PartialView(orders);
            return View(orders);
        }
        public ActionResult Finalize(int id)
        {
            Order order = db.Orders.Find(id);
            if (!order.Status.StartsWith("1")) return HttpNotFound();
            if (order == null)
                return HttpNotFound();

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
                }
            }

            if (order.User.Bank < order.Price)
            {
                ViewBag.Message = "موجودی حساب کاربر کافی نیست.";
            }


            ViewBag.Order = order.Product.Name;
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finalize(int Id, int OrderId, string Status)
        {
            if (Id != OrderId) return HttpNotFound();

            Order order = db.Orders.Find(OrderId);
            if (order == null) return HttpNotFound();

            User user = db.Users.Find(order.UserUsername);

            if (Status.StartsWith("2") && order.Status != Status)
            {
                /*این قسمت برای روز مباداست*/
                /*همان کار کاربر را انجام میدهد*/
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
            }
            else if (Status.StartsWith("4") && order.Status.StartsWith("2"))
            {
                /*پول کاربر را برمیگرداند*/
                Transaction _transaction = new Transaction();
                _transaction.Direction = true;
                _transaction.Date = About_Controller.CurrentDate;
                _transaction.UserUsername = order.UserUsername;
                _transaction.Value = order.Price;
                db.Transactions.Add(_transaction);
                db.SaveChanges();

                user.Bank = user.Bank + order.Price;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                _Admins.SendMessage(Session["Username"].ToString(), user.Username, "مبلغ " + order.Price + "به حساب شما بازگردانده شد.");
            }

            ((IObjectContextAdapter)db).ObjectContext.Detach(user);
            order.Status = Status;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Finalization");

            ViewBag.Message = "فرایند با شکست مواجه شد.";
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
    }
}
