using System.Collections.Generic;

namespace trainingservice.import.core.Interfaces.Models
{
  public class DbModel : EntityBase
  {
    public IEnumerable<TrackModel> Tracks { get; private set; }
  }
}
