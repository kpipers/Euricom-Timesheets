using System;
using System.Collections.Generic;
using System.Web.Http.Services;
using Ninject.Syntax;
using Ninject;
using Euricom.Timesheets.Infrastructure;
using Ninject.Web.Common;
using Ninject.Modules;

namespace Euricom.Timesheets.App_Start
{
    internal class NinjectResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll<Type>();
        }
    }   
}