using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Euricom.Timesheets.Models.Entities;
using Euricom.Timesheets.Util;
using System.Net.Http;
using System.Net;
using Euricom.Timesheets.Infrastructure;

namespace Euricom.Timesheets.Controllers.Api
{
    public class TimesheetsController : ApiController
    {
        IMongoContext _mongoContext;

        public TimesheetsController(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        // GET /api/timesheets/101
        [HttpGet]
        public Timesheet Get(long id)
        {
            var timesheet = new Timesheet();
            if (timesheet == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return timesheet;
        }

        // GET /api/timesheets/2012/6
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

        // POST /api/timesheets
        public HttpResponseMessage<Timesheet> Post(Timesheet timesheet)
        {
            var response = new HttpResponseMessage<Timesheet>(timesheet, HttpStatusCode.Created);

            var repository = _mongoContext.GetCollection<Timesheet>();
            repository.Insert(timesheet);

            // Where is the new timesheet?
            string uri = Url.Route(null, new { id = 101 });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            
            return response;
        }
    }
}