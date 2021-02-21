using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagement_Web_Site.Models
{
    public class ShiftsEmp
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public List<Employees> list { get; set; }

    }
}