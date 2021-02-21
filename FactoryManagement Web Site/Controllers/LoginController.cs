using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryManagement_Web_Site.Models;

namespace FactoryManagement_Web_Site.Controllers
{
    public class LoginController : Controller
    {
        UsersBL usersBL = new UsersBL();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                UserLogined user = (UserLogined)Session["user"];
                if(user.Logined == true && user.NumOfActions != 0)
                {
                    return RedirectToAction("HomePage");
                }
                else
                {
                    ViewBag.message = TempData["Message"];
                    return View("Login");
                }
            }
            else
            {
                ViewBag.message = TempData["Message"];
                return View("Login");
            }
           
        }
        public ActionResult HomePage()
        {

            if (Session["user"] != null)
            {
                UserLogined user = (UserLogined)Session["user"];
                if (user.Logined == true && user.NumOfActions != 0)
                {
                    return View("HomePage");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult CheckLogin(string usrname,string pswrd)
        {
            if (Session["user"] != null)
            {
                UserLogined user = (UserLogined)Session["user"];

                var datetime = DateTime.Now;
                var date = datetime.Date;

                var userlogindatetime = user.LoginDate;
                var userlogindate = userlogindatetime.Date;
                if(date != userlogindate)
                {
                    user.LoginDate = DateTime.Now;
                    var userBL = usersBL.AuthorizeUser(usrname, pswrd);
                    user.NumOfActions = userBL.NumOfActions;
                    Session["user"] = user;
                }

                if (user.Logined == true && user.NumOfActions != 0)
                {
                    return View("HomePage");
                }
                else
                {
                    TempData["Message"] = "You Have no action left for today, pls come back tomorrow";
                    return RedirectToAction("Index");
                }
            }
            else
            {

                var userBL = usersBL.AuthorizeUser(usrname, pswrd);
                if (userBL != null)
                {
                    UserLogined user = new UserLogined();
                    user.FullName = userBL.FullName;
                    user.UserName = userBL.UserName;
                    user.Logined = true;
                    user.LoginDate = DateTime.Now;
                    user.NumOfActions = userBL.NumOfActions;
                    Session["user"] = user;
                    Session["username"] = user.FullName;
                    return RedirectToAction("HomePage");
                }
                else
                {
                    TempData["Message"] = "You Are not authorized!";
                    return RedirectToAction("Index");
                }

            }

        }

        public ActionResult Logout()
        {
            UserLogined user = (UserLogined)Session["user"];
            user.Logined = false;
            Session["user"] = user;
            Session["username"] = null;
            return RedirectToAction("Index");
        }

    }
}