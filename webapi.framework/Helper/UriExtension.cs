namespace webapi.framework.Helper
{
  using System;

  public static class UriExtension
  {
    public static Uri Append(this Uri front, string back)
    {
      return new Uri(front, back);
    }
  }
}
