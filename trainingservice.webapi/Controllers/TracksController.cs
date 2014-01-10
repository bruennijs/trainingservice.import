using System.Web.Http;

namespace trainingservice.webapi.Controllers
{
  using System;
  using System.IO;
  using System.Net;
  using System.Net.Http;

  using Newtonsoft.Json;

  /// <summary>
  /// 
  /// </summary>
  [RoutePrefix("tracks")]
  public class TracksController : ApiController
  {

    [HttpPost]
    [Route("vdoimport")]
    public HttpResponseMessage PostVdoDatabaseFile([FromBody] Stream file)
    {
      HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
      response.Headers.Location = new Uri("tracks", UriKind.Relative);
      return response;
    }

    [HttpPost]
    [Route("")]

    [HttpGet]
    [Route("")]
    public HttpResponseMessage GetTracks()
    {
      return new HttpResponseMessage(HttpStatusCode.OK);
    }
  }
}
