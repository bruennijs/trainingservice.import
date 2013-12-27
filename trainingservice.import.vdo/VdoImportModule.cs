namespace trainingservice.vdoimport
{
  using System.Net.Configuration;

  using Autofac;
  using Autofac.Core;

  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.vdoimport.Repository;

  /// <summary>
  /// The vdo import module.
  /// </summary>
  public class VdoImportModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<VdoTrackRepository>().As<ITrackRepository>();
    }
  }
}
