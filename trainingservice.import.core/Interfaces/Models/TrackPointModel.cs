namespace trainingservice.import.core.Interfaces.Models
{
  using System;

  public class TrackPointModel : EntityBase
  {
    public int Distance { get; set; }

    public int Elevation { get; set; }

    public TimeSpan Time { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    /// <summary>
    /// Beats per minute.
    /// </summary>
    public int Heartrate { get; set; }

    /// <summary>
    /// Revolutions per minute.
    /// </summary>
    public int Cadence { get; set; }

    /// <summary>
    /// ID of the track segment this point is associated with.
    /// </summary>
    public int TrackSegmentId { get; set; }

    public int Power { get; set; }
  }
}