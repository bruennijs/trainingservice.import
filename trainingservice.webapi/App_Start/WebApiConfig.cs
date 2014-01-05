using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace trainingservice.webapi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "training/{action}",
          defaults: new { id = RouteParameter.Optional }
      );
    }
  }
}
