using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Euricom.Timesheets.Models.Entities;
using Euricom.Timesheets.Util;
using System.Net.Http;
using System.Net;
using Euricom.Timesheets.Infrastructure;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace Euricom.Timesheets.Controllers.Api
{
    public class TimesheetsController : ApiController
    {
        IMongoContext _mongoContext;

        public TimesheetsController(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        // GET /api/timesheets/4fd63a86f65e0a0e84e510de
        [HttpGet]
        public Timesheet Get(string id)
        {
            var repository = _mongoContext.GetCollection<Timesheet>();
            var query = Query.EQ("_id", new ObjectId(id));
            var timesheet = repository.Find(query).SingleOrDefault();
            if (timesheet == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return timesheet;
        }

        // GET /api/timesheets/2012/6
        [HttpGet]
        public Timesheet Get(int year, int month)
        {
            var repository = _mongoContext.GetCollection<Timesheet>();

            var query = Query.And(
                Query.EQ("Year", year),
                Query.EQ("Month", month));

            var timesheet = repository.Find(query).SingleOrDefault();
            if (timesheet == null)
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

                timesheet = new Timesheet 
                { 
                    Year = year, 
                    Month = month, 
                    WorkingDays = timesheetDays 
                };
            }

            return timesheet;
        }

        // POST /api/timesheets
        [HttpPost]
        public HttpResponseMessage<Timesheet> Post(Timesheet timesheet)
        {
            var response = new HttpResponseMessage<Timesheet>(timesheet, HttpStatusCode.Created);

            var repository = _mongoContext.GetCollection<Timesheet>();
            repository.Insert(timesheet);

            // Where is the new timesheet?
            string uri = Url.Route(null, new { id = timesheet.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            
            return response;
        }

        // PUT /api/timesheets
        [HttpPut]
        public HttpResponseMessage<Timesheet> Put(Timesheet timesheet)
        {
            var response = new HttpResponseMessage<Timesheet>(timesheet, HttpStatusCode.OK);

            var repository = _mongoContext.GetCollection<Timesheet>();

            var update = Update.Set("Name", timesheet.Name)
                               .Set("Year", timesheet.Year)
                               .Set("Month", timesheet.Month);

            // TODO: Update WorkingDays

            repository.Update(Query.EQ("_id", timesheet.Id), update);

            // Where is the modified timesheet?
            string uri = Url.Route(null, new { id = timesheet.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);

            return response;
        }
    }
}