using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagement_Web_Site.Models
{
    public class EmployeesBL
    {
        factoryDBEntities db = new factoryDBEntities();

        public List<ShiftsEmployee> GetAllEmployeeData()
        {
         List<ShiftsEmployee> list = new List<ShiftsEmployee>();

            List<Employee> emplist = db.Employees.ToList();
            
            foreach(var item in emplist)
            {
                  var result = from emp in db.Employees
                               join empshift in db.EmployeeShifts on emp.ID equals empshift.EmployeeID
                               join shift in db.Shifts on empshift.ShiftID equals shift.ID
                               where empshift.EmployeeID == item.ID
                               select new Shifts
                               {
                  
                                 Date       = (DateTime)shift.Date,
                                 StartTime  = shift.StartTime,
                                 EndTime    = shift.EndTime
                               
                                };
                List<Shifts> slist = new List<Shifts>();
                foreach (var x in result)
                {
                    Shifts s = x;
                    slist.Add(s);
                }


                ShiftsEmployee se = new ShiftsEmployee();
                se.EmployeeID = item.ID;
                se.FirstName = item.FirstName;
                se.LastName = item.LastName;
                se.StartWorkYear = item.StartWorkYear;
                se.DepartmentID = item.DepatmentID;
                se.list = slist;
                list.Add(se);
            }

            return list;
        }

              public List<ShiftsEmployee> GetEmployeesBySearch(string fname, string lname, Nullable<int> dep)
        {

            List<ShiftsEmployee> list = new List<ShiftsEmployee>();

            List<Employee> emplist = db.Employees.Where(x => x.FirstName.Contains(fname) && x.LastName.Contains(lname) && x.DepatmentID == dep ).ToList();
  //      List<Employee> emplist = new List<Employee>();
  //
  //      if(dep != null)
  //      {
  //          emplist = elist.Where(x => x.DepatmentID == dep).ToList();
  //      }
  //      else
  //      {
  //          emplist = elist;
  //      }

            foreach (var item in emplist)
            {
                var result = from emp in db.Employees
                             join empshift in db.EmployeeShifts on emp.ID equals empshift.EmployeeID
                             join shift in db.Shifts on empshift.ShiftID equals shift.ID
                             where empshift.EmployeeID == item.ID
                             select new Shifts
                             {

                                 Date = (DateTime)shift.Date,
                                 StartTime = shift.StartTime,
                                 EndTime = shift.EndTime

                             };
                List<Shifts> slist = new List<Shifts>();
                foreach (var x in result)
                {
                    Shifts s = x;
                    slist.Add(s);
                }


                ShiftsEmployee se = new ShiftsEmployee();
                se.EmployeeID = item.ID;
                se.FirstName = item.FirstName;
                se.LastName = item.LastName;
                se.StartWorkYear = item.StartWorkYear;
                se.DepartmentID = item.DepatmentID;
                se.list = slist;
                list.Add(se);
            }

            return list;


        }
   
            public Employee GetEmployeeByID(int id)
            {
                return db.Employees.Where(x => x.ID == id).First();
            }

            public void UpdateEmployee(Employee e, int dep)
            {
                var emp = db.Employees.Where(x => x.ID == e.ID).First();
                emp.FirstName = e.FirstName;
                emp.LastName = e.LastName;
                emp.StartWorkYear = e.StartWorkYear;
                emp.DepatmentID = dep;
                db.SaveChanges();
            }

        public void AddNewShift(EmployeeShift se, int id)
        {
            se.ShiftID = id;
            db.EmployeeShifts.Add(se);
            db.SaveChanges();
        }

            public void DeleteEmployee(int id)
            {
                var emp = db.Employees.Where(x => x.ID == id).First();
                var empshift = db.EmployeeShifts.Where(x => x.EmployeeID == id).ToList();
                var dep = db.Departments.Where(x => x.Manager == id).ToList();
               foreach (var item in dep)
               {
                   var d = db.Departments.Where(x => x.ID == item.ID).First();
                       d.Manager = null; 
               }

              foreach (var item in empshift)
                  {
                  var e = db.EmployeeShifts.Where(x => x.ID == item.ID).First();
                  db.EmployeeShifts.Remove(e);
                  }
                  db.Employees.Remove(emp);
                  db.SaveChanges();
              }
        }
    }
