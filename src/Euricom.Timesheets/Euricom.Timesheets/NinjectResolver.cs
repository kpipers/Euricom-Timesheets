using System;
using System.Collections.Generic;
using System.Web.Http.Services;
using Ninject.Syntax;
using Ninject;
using Euricom.Timesheets.Infrastructure;
using Ninject.Web.Common;
using Ninject.Modules;
using System.Web.Http.Dependencies;

namespace Euricom.Timesheets.Web
{
    public class NinjectResolver : NinjectScope, IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }   
}