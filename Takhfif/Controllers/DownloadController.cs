using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    public class DownloadController : Controller
    {
        public ActionResult Main(Guid id)
        {
            ModelContainer db = new ModelContainer();
            Upload upload = db.Uploads.Find(id);
            return File(upload.Bytes, upload.Type);
        }
    }
}
