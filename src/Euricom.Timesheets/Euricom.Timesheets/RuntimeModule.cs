using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Web.Common;
using Ninject.Modules;
using Euricom.Timesheets.Infrastructure;
using System.Web.Http.Dispatcher;
using System.Net.Http.Formatting;
using System.Web.Http.Services;
using System.Web.Http.Controllers;

namespace Euricom.Timesheets
{
    internal class RuntimeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMongoContext>().To<MongoContext>().InRequestScope();
        }
    }
}