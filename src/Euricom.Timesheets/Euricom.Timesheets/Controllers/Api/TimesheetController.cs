using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Euricom.Timesheets.Infrastructure;
using Euricom.Timesheets.Models.Entities;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Euricom.Timesheets.Controllers.Api
{
    public class TimesheetController : ApiController
    {
        IMongoContext context;
        public TimesheetController(IMongoContext context)
        {
            this.context = context;
        }
     
        // GET api/timesheet/5
        public Timesheet Get(string id)
        {
            return context.GetCollection<Timesheet>().FindOneById(new BsonObjectId(id));
        }

        // POST api/timesheet
        public string Post(Timesheet value)
        {          
            context.GetCollection<Timesheet>().Insert(value);
            return value.Id.ToString();
        }

        // PUT api/timesheet/5
        public void Put(int id, string value)
        {
        }       
    }
}
