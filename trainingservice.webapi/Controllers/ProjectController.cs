using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace trainingservice.webapi.Controllers
{
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Http.Results;

  using Newtonsoft.Json;

  using trainingservice.webapi.Dto;

  [RoutePrefix("projects")]
  public class ProjectController : ApiController
  {
    [HttpPost]
    [Route("")]
    public async Task<HttpResponseMessage> PostProject(HttpRequestMessage request)
    {
      string json = await request.Content.ReadAsStringAsync();
      Project project = JsonConvert.DeserializeObject<Project>(json);

      project.Id = Guid.NewGuid().ToString();

      ////return new OkResult(request);
      HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                                                  {
                                                    Content =
                                                      new StringContent(JsonConvert.SerializeObject(project))
                                                  };

      response.Headers.Location = new Uri(request.RequestUri, project.Id);

      return response;
    }

    [HttpGet]
    [Route("{id}")]
    public IHttpActionResult GetById(string id)
    {
      return new JsonResult<Project>(new Project() { Id = id }, new JsonSerializerSettings(), new ASCIIEncoding(), this);
    }

    [HttpGet]
    [Route("{id}.apiserialize")]
    public Project GetByIdApiSerialize(string id)
    {
      return new Project() { Id = id };
    }
  }
}
