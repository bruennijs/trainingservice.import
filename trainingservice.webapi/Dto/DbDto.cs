namespace trainingservice.webapi.Dto
{
  public class DbDto
  {
    public string Id { get; set; }

    public TrackSummaryDto[] Tracks { get; set; }
  }
}