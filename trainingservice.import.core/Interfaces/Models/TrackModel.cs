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

    public double Distance { get; set; }

    public double HeartRateAvg { get; set; }

    public double HeartRateMax { get; set; }

    public double Elavation { get; set; }
  }
}