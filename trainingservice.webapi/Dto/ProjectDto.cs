namespace trainingservice.webapi.Dto
{
  public class ProjectDto
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public TrackSummaryDto[] Tracks { get; set; } 
  }
}