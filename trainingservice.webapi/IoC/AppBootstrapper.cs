﻿namespace trainingservice.webapi.IoC
{
  using Autofac;

  using trainingservice.import.core;
  using trainingservice.vdoimport;
  using trainingservice.webapi.Controllers;

  public class AppBootstrapper
  {
    public AppBootstrapper(ContainerBuilder container)
    {
      RegisterModules(container);

      container.RegisterType<VdoImportController>();
    }

    private static void RegisterModules(ContainerBuilder container)
    {
      container.RegisterModule<VdoImportModule>();
      container.RegisterModule<ImportCoreModule>();
    }
  }
}