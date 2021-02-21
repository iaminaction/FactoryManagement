using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagement_Web_Site.Models
{
    public class DepartmentsBL
    {
        factoryDBEntities db = new factoryDBEntities();

        public List<Department> GetAllDepartments()
        {
            return db.Departments.ToList();
        }

        public List<DepartmentEnh> GetDepartments()
        {
            List<DepartmentEnh> list = new List<DepartmentEnh>();

            var dep = db.Departments.ToList(); 

            foreach(var item in dep)
            {
                DepartmentEnh d = new DepartmentEnh();
                d.ID      = item.ID;
                d.Name    = item.Name;
                d.Manager = item.Manager;

                var m = db.Employees.Where(x => x.ID == d.Manager).FirstOrDefault();
                if(m != null)
                {
                    d.ManagerName = m.FirstName + " " + m.LastName;

                }

                var e = db.Employees.Where(x => x.DepatmentID == d.ID).FirstOrDefault();
                if(e == null)
                {
                    d.del = true;
                }
                else
                {
                    d.del = false;
                }


                list.Add(d);
            }

            return list;
        }

        public Department GetDepartmentByID(int depID)
        {
            return db.Departments.Where(x => x.ID == depID).First();
        }

        public void UpdateDepartment(Department d, int mng)
        {
            var dep = db.Departments.Where(x => x.ID == d.ID).First();

            dep.Name = d.Name;
            dep.Manager = mng;
            db.SaveChanges();
        }

        public void CreateDepartment(Department d)
        {
            db.Departments.Add(d);
            db.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var dep = db.Departments.Where(x => x.ID == id).FirstOrDefault();
            db.Departments.Remove(dep);
            db.SaveChanges();

        }
    }
}