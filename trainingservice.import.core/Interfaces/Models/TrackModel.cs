namespace trainingservice.import.core.Interfaces.Models
{
  using System;

  public class TrackModel
  {
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public string Id { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan Duration { get; set; }

    public uint Distance { get; set; }

    public uint HeartRateAvg { get; set; }

    public uint HeartRateMax { get; set; }

    public uint Elavation { get; set; }
  }
}