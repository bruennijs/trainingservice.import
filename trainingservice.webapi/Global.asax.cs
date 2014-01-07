using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace trainingservice.webapi
{
  using Autofac;
  using Autofac.Integration.WebApi;

  using trainingservice.webapi.IoC;

  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      ////AreaRegistration.RegisterAllAreas();      

      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      ////BundleConfig.RegisterBundles(BundleTable.Bundles);

      this.RegisterDependencyInjectionContainer();
    }

    private void RegisterDependencyInjectionContainer()
    {
      ContainerBuilder builder = new ContainerBuilder();

      AppBootstrapper appBootstrapper = new AppBootstrapper(builder);

      AutofacWebApiDependencyResolver diContainer = new AutofacWebApiDependencyResolver(builder.Build());

      GlobalConfiguration.Configuration.DependencyResolver = diContainer;
    }
  }
}