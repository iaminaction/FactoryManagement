using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagement_Web_Site.Models
{
    public class ShiftsBL
    {
        EmployeesBL empBL = new EmployeesBL();
        factoryDBEntities db = new factoryDBEntities();

        public List<ShiftsEmp> GetShiftsData()
        {

            List<ShiftsEmp> list = new List<ShiftsEmp>();

            List<Shift> slist = db.Shifts.ToList();

            foreach (var item in slist)
            {
                var result = from emp in db.Employees
                             join empshift in db.EmployeeShifts on emp.ID equals empshift.EmployeeID
                             join shift in db.Shifts on empshift.ShiftID equals shift.ID
                             where empshift.ShiftID == item.ID
                             select new Employees
                             {

                                 ID = emp.ID,
                                 FullName = emp.FirstName + " " + emp.LastName

                             };
                List<Employees> elist = new List<Employees>();
                foreach (var x in result)
                {
                    Employees e = x;
                    elist.Add(e);
                }


                ShiftsEmp se = new ShiftsEmp();
                se.ID = item.ID;
                se.Date = item.Date;
                se.StartTime = item.StartTime;
                se.EndTime = item.EndTime;
                se.list = elist;
                list.Add(se);

              
            }
            return list;
        }
        public void AddShift(Shift s)
        {
            db.Shifts.Add(s);
            db.SaveChanges();
        }

    }
}