using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Models.Probl_message
{
    public class MessagesController : Controller
    {
        // GET: Message
        public ActionResult Messages()
        {
            return View();
        }
    }
}