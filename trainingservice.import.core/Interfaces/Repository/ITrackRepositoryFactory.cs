namespace trainingservice.import.core.Interfaces.Repository
{
  public interface ITrackRepositoryFactory
  {
    /// <summary>
    /// Creates the specified file path.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    ITrackRepository Create(string filePath);
  }
}