using System;

namespace trainingservice.webapi.Dto
{
  public class TrackSummaryDto
  {
    public string TrackId { get; set; }

    public DateTime Created { get; set; }

    public double Distance { get; set; }
  }
}