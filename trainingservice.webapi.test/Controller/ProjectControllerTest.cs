namespace trainingservice.webapi.test.Controller
{
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;

  using Newtonsoft.Json;

  using NUnit.Framework;

  using trainingservice.webapi.test.Dtos;

  [TestFixture]
  public class ProjectControllerTest
  {
    [Test]
    [TestCase("projects", "my project name")]
    public async void When_post_project_should_id_property_contain_guid(string url, string projectName)
    {
      Guid guid;
      ProjectDto projectDto = new ProjectDto(projectName);

      string json = JsonConvert.SerializeObject(projectDto);

      HttpResponseMessage response = await new HttpClient().PostAsync(
        new TestEnvironment().BaseUrl.Append(url),
        new StringContent(json));

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

      string jsonActual = await response.Content.ReadAsStringAsync();
      ProjectDto actualProjectDto = JsonConvert.DeserializeObject<ProjectDto>(jsonActual);

      Assert.IsTrue(Guid.TryParse(actualProjectDto.Id, out guid));
    }
  }
}
