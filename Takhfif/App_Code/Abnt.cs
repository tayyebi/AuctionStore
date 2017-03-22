
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takhfif.Controllers;

namespace Takhfif.App_Code
{
    public class Abnt
    {
        static public string WebApp_Url = Abnt_Controller.WebApp_Url;
        static public string WebApp_Name = Abnt_Controller.WebApp_Name;
        static public string WebApp_Key = Abnt_Controller.WebApp_Key;
        static public string ImageThumbnail(string Username)
        {
            return WebApp_Url + Username + "/en-US/Developer/ImageThumbnail";
        }
    }
}