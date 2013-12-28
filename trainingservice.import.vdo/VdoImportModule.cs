namespace trainingservice.vdoimport
{
  using Autofac;

  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.vdoimport.Repository;

  /// <summary>
  /// The vdo import module.
  /// </summary>
  public class VdoImportModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<VdoTrackRepositoryFactory>().As<ITrackRepositoryFactory>();
      builder.RegisterType<VdoTrackRepository>().As<ITrackRepository>();
    }
  }
}
