using System;
using System.Collections.Generic;

namespace trainingservice.import.core.Services
{
  using System.IO;

  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.import.core.Interfaces.Services;

  public class TrackExportServiceDecorator : ITrackExportService
  {
    public void Export(TrackModel track, IEnumerable<TrackPointModel> trackPoints, Stream outputStream)
    {
      throw new NotImplementedException();
    }
  }
}
