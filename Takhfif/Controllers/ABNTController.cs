using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Takhfif.Controllers
{
    public class ABNTController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult Status()
        {
            return PartialView();
        }
        public ActionResult Logout()
        {
            Session.Remove("Code");
            Session.Remove("Username");
            Session.Remove("Fullname");
            Session.Remove("Email");
            Session.Remove("Phone");
            Session.Remove("Address");
            Session.Remove("PostalCode");
            return Redirect("~/Abnt");
        }
        public ActionResult Index(string Username = null)
        {
            WebClient webclient = new WebClient();
            string Code = string.Empty;
            string _Username = string.Empty;
            string Fullname = string.Empty;
            string Email = string.Empty;
            string Phone = string.Empty;
            string Address = string.Empty;
            string PostalCode = string.Empty;

            try
            {
                Code = Session["Code"].ToString();
                _Username = Session["Username"].ToString();
            }
            catch { }
            if (Code != "" && Code != null)
            {
                if (_Username != "" && _Username != null)
                {
                }
                else if (_Username == "")
                {

                    string Url =  Abnt_Controller.WebApp_Url + "[webapp]/en-US/WebAppUser" + "?Name=" + Abnt_Controller.WebApp_Name + "&Key=" + Abnt_Controller.WebApp_Key + "&Code=" + Code + "&Export=";
                    try
                    {
                        _Username = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Username"));
                        Fullname = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Fullname"));
                        Email = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Email"));
                        Phone = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Phone"));
                        PostalCode = webclient.DownloadString(HttpUtility.UrlDecode(Url + "PostalCode"));
                        Address = webclient.DownloadString(HttpUtility.UrlDecode(Url + "Address"));
                    }
                    catch { }

                    if (_Username != "" && _Username != null)
                    {
                        Session["Username"] = _Username;
                        Session["Fullname"] = Fullname;
                        Session["Email"] = Email;
                        Session["PostalCode"] = PostalCode;
                        Session["Address"] = Address;
                        Session["Phone"] = Phone;

                        _Admins.SendMessage("[Abnabat]", _Username, "ورود موفقیت آمیز به سیستم در تاریخ " + About_Controller.CurrentDate);
                    }
                    else
                    {
                        Response.Redirect(Abnt_Controller.WebApp_Url + "[webapp]/en-US/WebAppAllow/Index" + "?Name=" + Abnt_Controller.WebApp_Name + "&Code=" + Code);
                    }
                }
            }
            else
            {
                try
                {
                    Session["Code"] = webclient.DownloadString(HttpUtility.UrlDecode(Abnt_Controller.WebApp_Url + "[webapp]/en-US/WebAppCode")).ToString();
                }
                catch
                {
                    ViewBag.Message = "دریافت اطلاعات... شکست!";
                }
            }
            return View();
        }
    }
}