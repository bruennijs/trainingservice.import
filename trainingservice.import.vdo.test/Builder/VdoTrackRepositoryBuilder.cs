namespace trainingservice.import.vdo.test.Builder
{
  using System.Data.OleDb;

  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.vdoimport.Repository;

  public class VdoTrackRepositoryBuilder
  {
    private string filePath = string.Empty;

    public ITrackRepository Build()
    {
      OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
      builder.DataSource = this.filePath;
      builder.Provider = "Provider=Microsoft.ACE.OLEDB.12.0";
      return new VdoTrackRepository(builder.ToString());
    }

    public VdoTrackRepositoryBuilder WithDatabaseFilePath(string value)
    {
      this.filePath = value;
      return this;
    }
  }
}