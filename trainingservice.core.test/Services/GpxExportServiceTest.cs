namespace trainingservice.core.test.Services
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Linq;
  using System.Xml.XPath;

  using NUnit.Framework;

  using trainingservice.core.test.Helper;
  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Services;

  [TestFixture]
  public class GpxExportServiceTest
  {
    private static readonly XNamespace GpxNs = "http://www.topografix.com/GPX/1/1";

    [Test]
    [TestCase("2014-01-18T00:00:00Z")]
    public void When_export_should_gpx_metadata_time(string value)
    {
      Stream outStream = CreateStream();

      DateTime expectedTime = DateTime.Parse(value);

      GpxExportService sut = new GpxExportServiceBuilder().Build();
      sut.Export(new TrackModel() { Date = expectedTime }, null, outStream);

      XDocument actual = Parse(outStream);

      Assert.AreEqual(value, actual.Root.Element(GpxNs + "metadata").Element(GpxNs + "time").Value);
    }

    [Test]
    [TestCase(5.67, 7.89)]
    public void When_export_should_gpx_trackpoint_lon_lat_correct(double lon, double lat)
    {
      Stream outStream = CreateStream();

      GpxExportService sut = new GpxExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Longitude = lon, Latitude = lat } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.TrackPoints().Single();
      
      Assert.AreEqual(lon, XmlConvert.ToDouble(tp.Attribute("lon").Value));
      Assert.AreEqual(lat, XmlConvert.ToDouble(tp.Attribute("lat").Value));
    }

    [Test]
    [TestCase(156)]
    public void When_export_should_trackpoint_heartrate(int value)
    {
      Stream outStream = CreateStream();

      GpxExportService sut = new GpxExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Heartrate = value } }, outStream);

      XDocument actual = Parse(outStream);

      Assert.AreEqual(value, actual.Root.TrackPoints().Single().Heartrate());
    }

    [Test]
    [TestCase(45)]
    public void When_export_should_trackpoint_elevation(int value)
    {
      Stream outStream = CreateStream();

      GpxExportService sut = new GpxExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Elevation = value } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.TrackPoints().Single();

      Assert.AreEqual(value, XmlConvert.ToInt32(tp.Element(GpxNs + "ele").Value));
    }

    [Test]
    [TestCase("2014-01-18T01:20:23Z")]
    public void When_export_should_trackpoint_time(string value)
    {
      Stream outStream = CreateStream();

      GpxExportService sut = new GpxExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Time = DateTime.Parse(value) } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.TrackPoints().Single();

      Assert.AreEqual(value, tp.Element(GpxNs + "time").Value);
    }



    private static XDocument Parse(Stream outStream)
    {
      outStream.Position = 0;
      return XDocument.Load(outStream);
    }

    private Stream CreateStream()
    {
      return new MemoryStream();
    }
  }

  public class GpxExportServiceBuilder
  {
    public GpxExportService Build()
    {
      return new GpxExportService();
    }
  }
}
