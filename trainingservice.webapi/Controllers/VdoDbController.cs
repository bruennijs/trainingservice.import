using System.Web.Http;

namespace trainingservice.webapi.Controllers
{
  using System.Net.Http;
  using System.Threading.Tasks;

  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.webapi.Dto;

  [RoutePrefix("databases")]
  public class VdoDbController : ApiController
  {
    private readonly IDbRepository dbRepository;

    private readonly ITrackRepositoryFactory trackRepositoryFactory;

    public VdoDbController(IDbRepository dbRepository)
    {
      this.dbRepository = dbRepository;
    }

    [HttpPost]
    [Route("{id}")]
    public Task<TrackSummaryDto[]> ImportVdoDb(HttpRequestMessage requestMessage)
    {
      this.trackRepositoryFactory.Create()

      return Task.FromResult(new [] { new TrackSummaryDto() });
    }
  }
}
