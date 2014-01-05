using System;

namespace trainingservice.webapi.test
{
  /// <summary>
  /// The test environment.
  /// </summary>
  internal class TestEnvironment
  {
    /// <summary>
    /// Gets the base url.
    /// </summary>
    public Uri BaseUrl
    {
      get { return new Uri("http://127.0.0.1:8090"); }
    }
  }
}
