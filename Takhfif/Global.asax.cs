using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Takhfif
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Error()
        {
            //Server.ClearError();
            //Server.Transfer("~/Error.html");
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Request_Start()
        {
            string Address = (HttpContext.Current.Request.Url.AbsolutePath + HttpContext.Current.Request.Url.Query).ToLower();
            if (Address.StartsWith("/admin"))
            {
                Address = Address.Replace("/admin", null);
                if (Address.StartsWith("/head") == false && Address.StartsWith("/police") == false)
                    Response.Redirect("~/");
            }
        }
    }
}