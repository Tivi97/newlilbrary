using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Models.Account
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult LibraryCard()
        {
            return View();
        }
    }
}