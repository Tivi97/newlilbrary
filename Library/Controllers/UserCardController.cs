using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class UserCardController : Controller
    {
        // GET: UserCard
        public ActionResult UserLibraryCard()
        {
            return View();
        }
    }
}