// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Gira, Giersiepen GmbH &amp; Co. KG">
//   Copyright (c) 2010 Gira, Giersiepen GmbH &amp; Co. KG. All rights reserved.
// </copyright>
// <author>ise GmbH</author>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace trainingservice.app
{
  using Autofac;
  using Autofac.Integration.WebApi;

  using trainingservice.app.IoC;

  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();

      WebApiConfig.Register(GlobalConfiguration.Configuration);
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