﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace trainingservice.webapi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();

      //// http://www.asp.net/web-api/overview/working-with-http/http-message-handlers
      ////config.MessageHandlers.Add(new MessageHandler1());

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "training/{action}",
          defaults: new { id = RouteParameter.Optional }
      );
    }
  }
}
