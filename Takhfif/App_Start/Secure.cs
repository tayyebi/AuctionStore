using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.Models;

namespace Takhfif.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]

    class SecureAttribute : ActionFilterAttribute, IActionFilter
    {
        #region properties
        public string UserType { get; set; }
        public bool Admin { get; set; }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool CanAccess = true;
            ModelContainer db = new ModelContainer();

            if (Admin)
            {
                string Address = (filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath + filterContext.HttpContext.Request.Url.Query).ToLower();
                if (!Address.StartsWith("/admin"))
                {
                    filterContext.Result = new HttpStatusCodeResult(404);
                    CanAccess = false;
                }
            }


            string Username = string.Empty;
            string Email = string.Empty;
            try
            {
                if (CanAccess)
                {
                    Username = filterContext.HttpContext.Session["Username"].ToString();
                    Email = filterContext.HttpContext.Session["Email"].ToString();
                }
            }
            catch
            {
                CanAccess = false;
            }


            if (Admin && CanAccess)
            {
                try
                {
                    var Administrator = db.Admins.Find(Username);
                    if (Administrator.Username.ToLower() != Username.ToLower())
                        CanAccess = false;
                    if (Administrator.Type != UserType && UserType != "*" && Administrator.Type != "Head")
                        CanAccess = false;
                }
                catch
                {
                    CanAccess = false;
                }
            }
            else if (!Admin && CanAccess)
            {
                try
                {
                    var User = db.Users.Find(Username).Username;
                    if (User == null)
                    {
                        CanAccess = false;
                    }
                }
                catch
                {
                    CanAccess = false;
                    User _user = new User();
                    _user.Username = Username;
                    _user.Email = Email;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    CanAccess = true;
                }
            }


            if (!CanAccess)
                filterContext.Result = new HttpStatusCodeResult(404);
        }
    }
}