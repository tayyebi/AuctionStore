using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Takhfif.Controllers
{
    public class About_Controller
    {
        static public string Name = "تخفیف طلایی";
        static public string Date(int Year = 0, int Month = 0, int Day = 0, int Hour = 0, int Minute = 0, int Second = 0)
        {
            DateTime Output = DateTime.Now.AddSeconds(Second).AddMinutes(Minute).AddHours(Hour).AddDays(Day).AddMonths(Month).AddYears(Year);
            return Output.ToString("yyyy/MM/dd-HH:mm:ss");
        }
        static public string CurrentDate = Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        static public string Root = HttpContext.Current.Request.Url.Scheme.ToString() + "://" + HttpContext.Current.Request.Url.Host.ToString() + ":" + HttpContext.Current.Request.Url.Port.ToString() + "/";
    }
}