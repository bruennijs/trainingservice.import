﻿namespace trainingservice.import.core
{
  using Autofac;

  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.import.core.Interfaces.Services;
  using trainingservice.import.core.Services;

  public class ImportCoreModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      ////builder.RegisterType<DbRepository>().As<IDbRepository>();      
    }
  }
}
