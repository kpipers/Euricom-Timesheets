using System;
using System.Collections.Generic;
using Euricom.Timesheets.Controllers.Api;
using Euricom.Timesheets.Infrastructure;
using Euricom.Timesheets.Models.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace Euricom.Timesheets.Test
{
    [TestClass]
    public class TimesheetControllerTest
    {
        [TestInitialize]
        public void setup()
        {
            var context = new MongoContext();
            context.GetCollection<Timesheet>().Drop();
        }

       // [TestMethod]
        public void Post_adds_new_timesheet()
        {
            var timesheetcontroller = new TimesheetController(new MongoContext());

            var responseId = timesheetcontroller.Post(new Timesheet()
            {
                Name = "Kristof Pipers",
                Month = "April",
                Days = new List<TimesheetDay>()
                {
                    new TimesheetDay(){Date = DateTime.Now , Type = "Werkdag"},
                    new TimesheetDay(){Date = DateTime.Now.AddDays(1) , Type = "Verlof"},
                    new TimesheetDay(){Date = DateTime.Now.AddDays(2) , Type = "Werkdag"}
                }
            });

            var ts = timesheetcontroller.Get(responseId);
            Assert.AreEqual(ts.Name, "Kristof Pipers");
            Assert.AreEqual(ts.Month, "April");
            Assert.AreEqual(ts.Days.Count, 3);
            Assert.IsNotNull(ts);
        }
    }
}
