using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Takhfif.Controllers
{
    static class StringExtensions
    {
        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("طول باید عددی مثبت باشد.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }
    }
    public class ComponentController : Controller
    {
        public PartialViewResult Clock(string id, bool Stable = false)
        {
            string Date;
            string currentDate = Date = DateTime.Now.ToString("yyyy,MM,dd,HH,mm,ss");

            if (id == null)
            {
                Date = currentDate;
            }
            else
            {
                Date = String.Join(",", id.SplitInParts(2));
                try
                {
                    Date = Date.Remove(2, 1);
                    Date = Date + "";
                }
                catch { }
            }
            ViewBag.currentDate = currentDate;
            ViewBag.futureDate = Date;

            if (Request.IsAjaxRequest())
                ViewBag.Stable = true;
            else ViewBag.Stable = Stable;

            return PartialView();
        }

    }
}
