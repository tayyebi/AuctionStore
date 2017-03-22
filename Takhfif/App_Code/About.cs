using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Takhfif.Controllers;

namespace Takhfif.App_Code
{
    public class About
    {
        static public string Name = About_Controller.Name;
        static public string Date(int Year = 0, int Month = 0, int Day = 0, int Hour = 0, int Minute = 0, int Second = 0)
        {
            return About_Controller.Date(Year, Month, Day, Hour, Minute, Second);
        }
        static public string CurrentDate = About_Controller.CurrentDate;
        static public string Root = About_Controller.Root;
    }
}