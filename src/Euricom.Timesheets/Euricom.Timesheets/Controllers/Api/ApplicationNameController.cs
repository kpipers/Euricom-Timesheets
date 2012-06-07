using System.Linq;
using System.Web.Http;
using Euricom.Timesheets.Infrastructure;
using Euricom.Timesheets.Models.Entities;
using System;
using Euricom.Core.Data;

namespace Euricom.Timesheets.Controllers.Api
{
    public class ApplicationNameController : ApiController
    {
        private readonly IRepository _repository;

        public ApplicationNameController(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }
        
        public ApplicationName Get()
        {
            return _repository.All<ApplicationName>().First();
        }              
        
        public void Put(string value)
        {
            var applicationName = _repository.All<ApplicationName>().First();
            if (applicationName == null)
            {
                applicationName = new ApplicationName();
            }
            applicationName.Value = value;
            _repository.Save<ApplicationName>(applicationName);
        }    
    }
}
