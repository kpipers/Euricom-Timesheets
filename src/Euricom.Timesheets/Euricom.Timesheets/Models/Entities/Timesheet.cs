using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Euricom.Timesheets.Models.Entities
{
    public class Timesheet : Entity
    {
        public string Name { get; set; }
        public string Month { get; set; }
        public IList<TimesheetDay> Days { get; set; }
    }
}