namespace trainingservice.app.IoC
{
  using Autofac;
  using Autofac.Integration.WebApi;

  using trainingservice.app.Controllers;
  using trainingservice.import.core;
  using trainingservice.vdoimport;

  public class AppBootstrapper
  {
    public AppBootstrapper(ContainerBuilder container)
    {
      RegisterModules(container);

      container.RegisterType<TrainingImportController>().InstancePerApiRequest();
    }

    private static void RegisterModules(ContainerBuilder container)
    {
      container.RegisterModule<VdoImportModule>();
      container.RegisterModule<ImportCoreModule>();
    }
  }
}