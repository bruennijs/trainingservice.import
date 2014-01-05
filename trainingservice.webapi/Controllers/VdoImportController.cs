using System.Web.Http;

namespace trainingservice.webapi.Controllers
{
  using System;
  using System.IO;
  using System.Net;
  using System.Net.Http;

  public class VdoImportController : ApiController
  {
    [HttpPost]
    [ActionName("vdoimport")]
    public HttpResponseMessage PostVdoDatabaseFile([FromBody] Stream file)
    {
      HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
      response.Headers.Location = new Uri("tracks", UriKind.Relative);
      return response;
    }
  }
}
