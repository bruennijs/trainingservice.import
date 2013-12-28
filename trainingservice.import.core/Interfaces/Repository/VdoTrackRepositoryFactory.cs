using System;

namespace trainingservice.import.core.Interfaces.Repository
{
  using System.IO;

  public class VdoTrackRepositoryFactory
  {
    /// <summary />
    private readonly Func<string, ITrackRepository> repositoryCreator;

    /// <summary>
    /// Initializes a new instance of the <see cref="VdoTrackRepositoryFactory"/> class.
    /// </summary>
    /// <param name="repositoryCreator">
    /// The repository creator.
    /// </param>
    public VdoTrackRepositoryFactory(Func<string, ITrackRepository> repositoryCreator)
    {
      this.repositoryCreator = repositoryCreator;
    }

    public ITrackRepository Create(string filePath)
    {
      return this.repositoryCreator("Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + filePath);
    }
  }
}
