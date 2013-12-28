namespace trainingservice.import.core.Interfaces.Repository
{
  using System.Collections.Generic;

  using trainingservice.import.core.Interfaces.Models;

  /// <summary>
  /// 
  /// </summary>
  public interface ITrackRepository
  {
    IEnumerable<TrackModel> GetTracks();
  }
}