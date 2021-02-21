using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryManagement_Web_Site.Models;

namespace FactoryManagement_Web_Site.Controllers
{
    public class ShiftController : Controller
    {

        ShiftsBL shiftsBL = new ShiftsBL();
        UsersBL ubl = new UsersBL();
        // GET: Shiift
        public ActionResult Index()
        {
            UserLogined u = (UserLogined)Session["user"];
            if (u != null)
            {

                if (u.Logined == true && u.NumOfActions != 0)
                {
                    ubl.UpdateActions(u);
                    Session["user"] = u;
                    var shifts = shiftsBL.GetShiftsData();
                    ViewBag.shifts = shifts;
                    return View("Shifts");
                }
                else
                {
                    TempData["Message"] = "You Have no action left for today, pls come back tomorrow";
                    return RedirectToAction("Logout", "Login");
                }
  
            }
            else
            {
                return RedirectToAction("Login", "Index");
            }
 
        }

        public ActionResult AddShift()
        {
            return View("AddShift");
        }
        public ActionResult GetNewShiftData( Shift s )
        {
           shiftsBL.AddShift(s);
            return RedirectToAction("Index");
        }

        public ActionResult EditEmp(int id)
        {
            return RedirectToAction("EditEmployee", "Employee", new { id = id });
        }
        
    }
}