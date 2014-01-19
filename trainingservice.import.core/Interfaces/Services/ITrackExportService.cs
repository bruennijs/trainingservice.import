using System.Collections.Generic;

namespace trainingservice.import.core.Interfaces.Services
{
  using System.IO;

  using trainingservice.import.core.Interfaces.Models;

  public enum ContainerType
  {
    Gpx,
    Tcx
  }

  public interface ITrackExportService
  {
    void Export(TrackModel track, IEnumerable<TrackPointModel> trackPoints, Stream outputStream);
  }
}
