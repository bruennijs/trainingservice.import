using System.Web.Http;

namespace trainingservice.webapi.Controllers
{
  using System.Threading.Tasks;

  using trainingservice.webapi.Dto;

  [RoutePrefix("projects/{projectid}/vdo")]
  public class VdoDbController : ApiController
  {
    [HttpPost]
    [Route("import")]
    public Task<TrackSummaryDto[]> ImportVdoDb(string projectid)
    {
      return Task.FromResult(new [] { new TrackSummaryDto() });
    }
  }
}
