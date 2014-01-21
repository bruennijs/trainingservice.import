using System.Web.Http;

namespace trainingservice.webapi.Controllers
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Net.Http;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Http.Results;

  using global::webapi.framework.Helper;
  using global::webapi.framework.REST;

  using Newtonsoft.Json;

  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.webapi.Dto;

  ////[RoutePrefix("databases")]
  public class VdoDbController : ApiController
  {
    private const string TracksResourceUrl = "tracks";

    private readonly IDbRepository dbRepository;

    private readonly ITrackRepositoryFactory trackRepositoryFactory;

    public VdoDbController(IDbRepository dbRepository)
    {
      this.dbRepository = dbRepository;
    }

    [HttpPost]
    [Route("databases/{id}")]
    public async Task<IHttpActionResult> ImportVdoDb(string id, HttpRequestMessage request)
    {
      ////this.trackRepositoryFactory.Create()
      if (!request.Content.IsMimeMultipartContent())
      {
        return new BadRequestResult(request);
      }

      MultipartContent multipartContent = (MultipartContent)request.Content;

      if (multipartContent.Count() != 1)
      {
        return new BadRequestErrorMessageResult("Only one file can be posted at a http request!", this);
      }

      Stream content = await multipartContent.Single().ReadAsStreamAsync();

      DbModel dbModel = this.dbRepository.Create(content);

      return
        new JsonResult<DbDto>(
          new DbDto()
            {
              Id = dbModel.Id,
              Links =
                new[]
                  {
                    new SemanticLink()
                      {
                        Action = "tracks",
                        Resource = request.RequestUri.Append(TracksResourceUrl)
                      }
                  }
            },
          new JsonSerializerSettings(),
          Encoding.ASCII,
          request);
    }
  }
}
