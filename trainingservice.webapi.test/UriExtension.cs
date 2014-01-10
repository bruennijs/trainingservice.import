using System;

namespace trainingservice.webapi.test
{
  public static class UriExtension
  {
    public static Uri Append(this Uri front, string back)
    {
      return new Uri(front, back);
    }
  }
}
