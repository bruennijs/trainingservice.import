namespace trainingservice.import.core.Interfaces.Models
{
  using System;

  public class TrackModel : EntityBase
  {
    public DateTime Date { get; set; }

    public TimeSpan Duration { get; set; }

    public double Distance { get; set; }

    public int HeartRateAvg { get; set; }

    public int HeartRateMax { get; set; }

    public int Elavation { get; set; }

    public int CadenceAvg { get; set; }
  }
}