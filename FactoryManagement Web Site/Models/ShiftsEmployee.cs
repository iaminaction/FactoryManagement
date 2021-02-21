using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagement_Web_Site.Models
{
    public partial class ShiftsEmployee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartWorkYear { get; set; }
        public Nullable<int> DepartmentID { get; set; }

        public List<Shifts> list { get; set; }

    }
}