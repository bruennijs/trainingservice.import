using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace trainingservice.webapi.Controllers
{
  using System.IO;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Http.Results;
  using System.Web.SessionState;

  using Newtonsoft.Json;

  using trainingservice.webapi.Dto;

  [RoutePrefix("projects")]
  public class ProjectController : ApiController, IRequiresSessionState
  {
    [HttpPost]
    [Route("{id}/vdoimport")]
    public HttpResponseMessage PostVdoDatabaseFile([FromBody] Stream file)
    {
      HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
      response.Headers.Location = new Uri("tracks", UriKind.Relative);
      return response;
    }

    [HttpPost]
    [Route("")]
    public async Task<HttpResponseMessage> PostProject(HttpRequestMessage request)
    {
      string json = await request.Content.ReadAsStringAsync();
      ProjectDto projectDto = JsonConvert.DeserializeObject<ProjectDto>(json);

      projectDto.Id = Guid.NewGuid().ToString();

      ////return new OkResult(request);
      HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                                                  {
                                                    Content =
                                                      new StringContent(JsonConvert.SerializeObject(projectDto))
                                                  };

      response.Headers.Location = new Uri(request.RequestUri, projectDto.Id);

      return response;
    }

    [HttpGet]
    [Route("{id}")]
    public IHttpActionResult GetById(string id)
    {
      return new JsonResult<ProjectDto>(new ProjectDto() { Id = id }, new JsonSerializerSettings(), new ASCIIEncoding(), this);
    }

    [HttpGet]
    [Route("{id}.apiserialize")]
    public ProjectDto GetByIdApiSerialize(string id)
    {
      return new ProjectDto() { Id = id };
    }
  }
}
