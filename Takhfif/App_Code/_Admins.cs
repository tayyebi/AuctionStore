using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Takhfif.Models;

namespace Takhfif.Controllers
{
    public class _Admins
    {
        static public void SendMessage(string FromUsername, string ToUsername, string Message)
        {
            InMail inmail = new InMail();
            inmail.IsRead = false;
            inmail.Date = About_Controller.CurrentDate;
            inmail.FromUsername = FromUsername;
            inmail.ToUsername = ToUsername;
            inmail.Value = Message;
            ModelContainer db = new ModelContainer();
            db.InMails.Add(inmail);
            db.SaveChanges();
            FromUsername = ToUsername = Message = null;
        }
    }
}