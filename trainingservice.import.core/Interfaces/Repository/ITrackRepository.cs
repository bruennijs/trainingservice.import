namespace trainingservice.import.core.Interfaces.Repository
{
  using System.Collections.Generic;

  using trainingservice.import.core.Interfaces.Models;

  /// <summary>
  /// Gets the tracks of a db.
  /// </summary>
  public interface ITrackRepository
  {
    IEnumerable<TrackModel> GetTracks();

    IEnumerable<TrackPointModel> GetTrackPoints(TrackModel track);
  }
}