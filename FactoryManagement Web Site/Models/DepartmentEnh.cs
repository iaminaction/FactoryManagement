using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagement_Web_Site.Models
{
    public partial class DepartmentEnh
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Manager { get; set; }

        public string ManagerName { get; set; }
        public bool del { get; set; }


    }
}