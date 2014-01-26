namespace trainingservice.integrationtest
{
  using System.IO;
  using System.Linq;

  using NUnit.Framework;

  using trainingservice.core.test.Services;
  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.import.core.Services;
  using trainingservice.import.vdo.test.Builder;

  [TestFixture]
  public class Vdo2GpxExportTest
  {
    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "3", @"C:\Temp\deleteme\vdo1_3.gpx")]
    public void When_export_should_gpx_created(string filePath, string id, string gpxFilePath)
    {
      ITrackRepository repo = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      GpxExportService export = new GpxExportServiceBuilder().Build();

      using (FileStream fs = File.OpenWrite(gpxFilePath))
      {
        TrackModel tm = repo.GetTracks().Single(m => m.Id == id);

        export.Export(tm, repo.GetTrackPoints(tm), fs);
      }
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "3", @"C:\Temp\deleteme\vdo1_3.tcx")]
    public void When_export_should_tcx_created(string filePath, string id, string gpxFilePath)
    {
      ITrackRepository repo = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      TCXv2ExportService export = new TCXv2ExportServiceBuilder().Build();

      using (FileStream fs = File.OpenWrite(gpxFilePath))
      {
        TrackModel tm = repo.GetTracks().Single(m => m.Id == id);

        export.Export(tm, repo.GetTrackPoints(tm), fs);
      }
    }
  }
}
