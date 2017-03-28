using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SchoolService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Castle.Windsor.IWindsorContainer _container; 
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterWindsonDiConfiguration(GlobalConfiguration.Configuration);
        }

        public static void RegisterWindsonDiConfiguration(HttpConfiguration config)
        {
            
            _container = new Castle.Windsor.WindsorContainer();
            _container.Install(Castle.Windsor.Installer.FromAssembly.This());
            Castle.Windsor.WindsorContainer container = new Castle.Windsor.WindsorContainer();
            config.DependencyResolver = new DiResolver(_container);

        }

    }
}