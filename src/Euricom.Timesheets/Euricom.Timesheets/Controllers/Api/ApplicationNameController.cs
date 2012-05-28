using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Euricom.Timesheets.Infrastructure;
using Euricom.Timesheets.Models.Entities;
using MongoDB.Driver.Builders;

namespace Euricom.Timesheets.Controllers.Api
{
    public class ApplicationNameController : ApiController
    {
        private IMongoContext _mongoContext;

        public ApplicationNameController()
        {
            // No DI for now, can be added later
            _mongoContext = new MongoContext();
        }
        
        public ApplicationName Get()
        {
            return _mongoContext.GetCollection<ApplicationName>().FindAll().First();
        }              
        
        public void Put(string value)
        {
            _mongoContext.GetCollection<ApplicationName>().Drop();
            _mongoContext.GetCollection<ApplicationName>().Insert(new ApplicationName() { Value = value });
        }    
    }
}
