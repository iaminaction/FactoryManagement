using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryManagement_Web_Site.Models;

namespace FactoryManagement_Web_Site.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentsBL depBL = new DepartmentsBL();
        EmployeesBL empBL = new EmployeesBL();
        UsersBL uBL = new UsersBL();
        // GET: Department
        public ActionResult Index()
        {
            UserLogined u = (UserLogined)Session["user"];
            if (u != null)
            {
                if (u.Logined == true && u.NumOfActions != 0)
                {
                    uBL.UpdateActions(u);
                    Session["user"] = u;
                    var departmetns = depBL.GetDepartments();
                    ViewBag.departments = departmetns;
                    return View("Departments");

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

        public ActionResult EditDepartment(int depID)
        {
            var employees = empBL.GetAllEmployeeData();
            ViewBag.employees = employees;
            Department d = depBL.GetDepartmentByID(depID);
            return View("EditDepartment", d);
        }
        public ActionResult DeleteDepartment(int depID)
        {
            depBL.DeleteDepartment(depID);
            return RedirectToAction("Index");
        }
        public ActionResult AddDepartment()
        {
            var employees = empBL.GetAllEmployeeData();
            ViewBag.employees = employees;
            return View("AddDepartment");
        }
        [HttpPost]
        public ActionResult GetUpdatedDepartment(Department d, int mng)
        {
            depBL.UpdateDepartment(d, mng);
            return RedirectToAction("Index");
        }

        public ActionResult GetNewDepartment(Department d)
        {
            depBL.CreateDepartment(d);

            return RedirectToAction("Index");
        }
    }
}