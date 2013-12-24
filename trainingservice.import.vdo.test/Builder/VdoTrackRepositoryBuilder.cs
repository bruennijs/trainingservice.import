namespace trainingservice.import.vdo.test.Builder
{
  using System.Data.Odbc;

  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.vdoimport.Repository;

  public class VdoTrackRepositoryBuilder
  {
    private string filePath = string.Empty;

    public ITrackRepository Build()
    {
      return new VdoTrackRepository("Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + this.filePath);
    }

    public VdoTrackRepositoryBuilder WithDatabaseFilePath(string value)
    {
      this.filePath = value;
      return this;
    }
  }
}