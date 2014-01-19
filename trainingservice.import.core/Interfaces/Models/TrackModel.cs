namespace trainingservice.import.core.Interfaces.Models
{
  using System;

  public class TrackModel : EntityBase
  {
    public DateTime Date { get; set; }

    public TimeSpan Duration { get; set; }

    public double Distance { get; set; }

    public double HeartRateAvg { get; set; }

    public double HeartRateMax { get; set; }

    public double Elavation { get; set; }
  }
}