using Library.Models.Account;
using System.Web;
using System.Web.Mvc;
using static Library.Models.Account.PersonModel;

//контролер "всевидяще око"
namespace Library.Controllers
{
    public class AccountController : ParentController
    {
       

        public ActionResult Index()
        {
            return Redirect("/Account/Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }




        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthModel user)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = new AccountModel().Auth(user.Username, user.Password);
                if (Session["login"] == null)
                {
                    return View();
                }
                else
                {
                    return Redirect("/Card/LibraryCard");
                }
            }
            else
            {
                SaveMessages();
                return View();
            }

        }

        public ActionResult Logout()
        {
            Session["admin"] = null;
            Session["coach"] = null;
            Session["login"] = null;
            Session["id"] = null;
            return Redirect("/Account/Login");
        } //а был ли мальчик




        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = new AccountModel().Register(user.Fio, user.Birthday, user.PassportSeries, user.PassportKod, user.CertificateCode, user.Email, user.userRole, user.Login, user.Password);

                if (Session["login"] == null)
                {
                    return View();
                }
                else
                {
                    return Redirect("/Card/LibraryCard");
                }
            }
            else
            {
                SaveMessages();
                return View();
            }
        }



        //[HttpGet]
        //public ActionResult UserProfile()
        //{
        //    if (Session["login"] != null)
        //    {
                
        //        return View(new AccountModel().GetProfile(Session["id"]));
        //    }
        //    else
        //    {
        //        TempData["message"] = "Вы не авторизованы!";
        //        return Redirect("/Account/Login");
        //    }
        //}

        //[HttpPost]
        //public ActionResult UserProfile(ProfileModel profile)
        //{
        //    if (Session["login"] != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            TempData["message"] = new AccountModel().UpdateProfile(profile, Session["id"]);
        //        }
        //        else
        //        {
        //            SaveMessages();
        //        }
        //        return Redirect("/Account/UserProfile");
        //    }
        //    else
        //    {
        //        TempData["message"] = "Вы не авторизованы!";
        //        return Redirect("/Account/Login");
        //    }
        //}



        ////        public ActionResult GetPdf()
        ////        {
        ////            if (Session["login"] != null)
        ////            {
        ////                Response.Clear();
        ////                Response.ContentType = "application/pdf";
        ////                Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf");
        ////                Response.Cache.SetCacheability(HttpCacheability.NoCache);
        ////                Response.BinaryWrite(new AccountModel().GetPdf());
        ////                Response.End();
        ////                return Redirect("/Account/UserAccount");
        ////            }
        ////            else
        ////            {
        ////                TempData["message"] = "Вы не авторизованы!";
        ////                return Redirect("/Account/Login");
        ////            }

        ////        }

        ////    }
        ////}


    }
}




//     