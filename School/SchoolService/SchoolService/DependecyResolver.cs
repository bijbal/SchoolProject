using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Lifestyle;

namespace SchoolService
{

    public class DiResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        Castle.Windsor.IWindsorContainer container;
        public DiResolver(Castle.Windsor.IWindsorContainer kern)
        {
            this.container = kern;
        }
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(container);
        }

        public object GetService(Type serviceType)
        {
            return container.Kernel.HasComponent(serviceType) ? container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType).Cast<object>().ToArray();
        }

        public void Dispose()
        {

        }
    }

    internal sealed class WindsorDependencyScope : System.Web.Http.Dependencies.IDependencyScope
    {
        private readonly Castle.Windsor.IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(Castle.Windsor.IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
            _scope = container.BeginScope();
        }

        public object GetService(Type t)
        {
            return _container.Kernel.HasComponent(t) ? _container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return _container.ResolveAll(t).Cast<object>().ToArray();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}