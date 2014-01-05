using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trainingservice.webapi.test.Controller
{
  using System.IO;
  using System.Net.Http;

  using NUnit.Framework;

  [Category("Integration")]
  [TestFixture]
  public class VdoImportControllerTest
  {
    [Test]
    [TestCase("training/vdoimport")]
    public void When_post_dbfile_should_return_location_with_tracks_collection(string url)
    {
      using (var fs = File.OpenRead(@".\TestFiles\vdo1.pcs"))
      {
        Task<HttpResponseMessage> task = new HttpClient().PostAsync(BuildVdoImportUrl(url), new StreamContent(fs));
        task.Wait();
        HttpResponseMessage response = task.Result;
        Assert.AreEqual(new Uri(new TestEnvironment().BaseUrl, "tracks"), response.Headers.Location);
      }
    }

    private static Uri BuildVdoImportUrl(string url)
    {
      return new Uri(new TestEnvironment().BaseUrl, url);
    }
  }
}
