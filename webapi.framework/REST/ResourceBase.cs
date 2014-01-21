namespace webapi.framework.REST
{
  using System;

  public class ResourceBase
  {
    /// <summary>
    /// Each resource should have an resourcev id.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the url of this resource.
    /// </summary>
    public Uri Self { get; set; }

    /// <summary>
    /// Gets or sets the links that 
    /// are accessable from this resource to.
    /// </summary>
    public SemanticLink[] Links { get; set; }
  }
}