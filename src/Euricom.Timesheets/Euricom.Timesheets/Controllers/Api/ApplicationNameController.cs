using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Euricom.Timesheets.Controllers.Api
{
    public class ApplicationNameController : ApiController
    {
        // GET /api/applicationname
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET /api/applicationname/5
        public string Get(int id)
        {
            return "value";
        }

        // POST /api/applicationname
        public void Post(string value)
        {
        }

        // PUT /api/applicationname/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/applicationname/5
        public void Delete(int id)
        {
        }
    }
}
