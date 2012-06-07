using System;
using System.Web;
using Ninject.Web.Common;
using Ninject.Modules;
using Euricom.Timesheets.Infrastructure;
using System.Web.Http.Dispatcher;
using System.Web.Http.Services;
using Euricom.Core.Data;
using Euricom.Core.Data.Mongo;
using System.Configuration;

namespace Euricom.Timesheets
{
    internal class RuntimeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMongoContext>().To<MongoContext>().InRequestScope();
            Bind<IRepository>().To<MongoRepository>().InRequestScope().WithConstructorArgument("connectionString", ConfigurationManager.AppSettings["MONGOHQ_URL"]);
        }
    }
}