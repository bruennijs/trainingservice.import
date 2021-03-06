﻿using System;
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
  public class VdoDbControllerTest
  {
    [Test]
    [TestCase("databases/4711")]
    public void When_post_dbfile_should_return_location_with_tracks_collection(string url)
    {
      using (var fs = File.OpenRead(@".\TestFiles\vdo1.pcs"))
      {
        MultipartContent multipartContent = new MultipartContent("vdo+msaccess");
        multipartContent.Add(new StreamContent(fs));
        Task<HttpResponseMessage> task = new HttpClient().PostAsync(BuildVdoImportUrl(url), multipartContent);
        task.Wait();
        HttpResponseMessage response = task.Result;
        Assert.AreEqual(new Uri(new TestEnvironment().BaseUrl, "tracks"), response.Headers.Location);
      }
    }

    [Test]
    public void When_post_dbfile_should_location_url_return_expected_tracks_in_json()
    {
      ////Newtonsoft.Json.
    }

    private static Uri BuildVdoImportUrl(string url)
    {
      return new Uri(new TestEnvironment().BaseUrl, url);
    }
  }
}
