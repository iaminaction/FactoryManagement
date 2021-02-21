using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryManagement_Web_Site.Models;

namespace FactoryManagement_Web_Site.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeesBL empBL = new EmployeesBL();
        DepartmentsBL depBL = new DepartmentsBL();
        ShiftsBL sBL = new ShiftsBL();
        UsersBL uBL = new UsersBL();

        // GET: Employee
        public ActionResult Index()
        {
            UserLogined u = (UserLogined)Session["user"];
            if (u != null)
            {
                if(u.Logined == true && u.NumOfActions != 0)
                        {
                    uBL.UpdateActions(u);
                    Session["user"] = u;
                    var departments = depBL.GetAllDepartments();
                    var employees = empBL.GetAllEmployeeData();
                    ViewBag.employees = employees;
                    ViewBag.departments = departments;
                    return View("Employees");
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

        public ActionResult EditEmployee(int id)
        {
            var dep = depBL.GetAllDepartments();
            var emp = empBL.GetEmployeeByID(id);
            ViewBag.departments = dep;
            return View("EditEmployee", emp);

        }
        [HttpPost]
        public ActionResult GetUpdatedEmployee(Employee e, int dep)
        {
            var departments = depBL.GetAllDepartments();
            ViewBag.departments = departments;
            empBL.UpdateEmployee(e, dep);
            return RedirectToAction("Index");
        }

        public ActionResult AddShiftToEmployee(int id)
        {
            Employee e = empBL.GetEmployeeByID(id);
            ViewBag.name = e.FirstName + e.LastName;
            var shifts = sBL.GetShiftsData();
            ViewBag.shifts = shifts;
            EmployeeShift se = new EmployeeShift();
            se.EmployeeID = id;
            return View("AddShiftToEmployee", se);
        }
        public ActionResult GetNewEmployeeShift(EmployeeShift es, int shiftID) 
        {

            empBL.AddNewShift(es, shiftID);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteEmployee(int ID)
            {
            empBL.DeleteEmployee(ID);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SearchResults( string fname, string lname, Nullable<int> dep)
        {
            var employees = empBL.GetEmployeesBySearch(fname,lname,dep);
            ViewBag.employees = employees;
            return View("SearchResults");
        }
    }
}