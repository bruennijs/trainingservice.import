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
  [RoutePrefix("projects/{projectid}/tracks")]
  public class TracksController : ApiController
  {
    [HttpGet]
    [Route("")]
    public HttpResponseMessage GetTracks(string projectid)
    {
      return new HttpResponseMessage(HttpStatusCode.OK);
    }
  }
}
