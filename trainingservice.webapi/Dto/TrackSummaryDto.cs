using System;

namespace trainingservice.webapi.Dto
{
  using global::webapi.framework.REST;

  public class TrackSummaryDto : ResourceBase
  {
    public DateTime Created { get; set; }

    public double Distance { get; set; }
  }
}