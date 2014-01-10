namespace trainingservice.webapi.test.Dtos
{
  public class ProjectDto
  {
    public ProjectDto(string projectName)
    {
      this.Name = projectName;
    }

    public string Name { get; set; }

    public string Id { get; set; }
  }
}