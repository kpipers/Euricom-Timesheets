using System.Linq;
using System.Web.Http;
using Euricom.Timesheets.Infrastructure;
using Euricom.Timesheets.Models.Entities;
using System;

namespace Euricom.Timesheets.Controllers.Api
{
    public class ApplicationNameController : ApiController
    {
        private readonly IMongoContext _mongoContext;

        public ApplicationNameController(IMongoContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _mongoContext = context;
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
