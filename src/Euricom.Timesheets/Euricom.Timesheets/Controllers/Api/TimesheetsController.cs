using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Euricom.Timesheets.Models.Entities;
using Euricom.Timesheets.Util;

namespace Euricom.Timesheets.Controllers.Api
{
    public class TimesheetsController : ApiController
    {
        [HttpGet]
        public IEnumerable<TimesheetDay> Get(int year, int month)
        {
            var days = Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                .Select(day => new DateTime(year, month, day)) // Map each day to a date
                .ToList(); // Load dates into a list

            var timesheetDays = new List<TimesheetDay>();
            days.ForEach(
                d =>
                timesheetDays.Add(new TimesheetDay
                {
                    Date = d,
                    IsWeekend = d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday
                }));

            return timesheetDays;
        }
    }
}