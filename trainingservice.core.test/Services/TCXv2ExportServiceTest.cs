namespace trainingservice.core.test.Services
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Linq;

  using NUnit.Framework;  

  using trainingservice.core.test.Helper.TcxV2;
  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Services;

  [TestFixture]
  public class TCXv2ExportServiceTest
  {
    private static readonly XNamespace TargetNs = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2";

    private static readonly XNamespace TpExtNs = "http://www.garmin.com/xmlschemas/TrackPointExtension/v1";

    [Test]
    [TestCase("2014-01-18T00:00:00Z")]
    public void When_export_should_trackmodel_time(string value)
    {
      Stream outStream = CreateStream();

      DateTime expectedTime = DateTime.Parse(value);

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel() { Date = expectedTime }, null, outStream);

      XDocument actual = Parse(outStream);

      Assert.AreEqual(value, actual.Root.Activities().First().Element(TargetNs + "Id").Value);
      Assert.AreEqual(value, actual.Root.Activities().First().Laps().First().Attribute("StartTime").Value);
    }

    [Test]
    [TestCase(123555.75)]
    public void When_export_should_trackmodel_distancemeters(double value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel() { Distance = value }, null, outStream);

      XDocument actual = Parse(outStream);

      Assert.AreEqual(value, XmlConvert.ToDouble(actual.Root.Activities().First().Laps().First().Element(TargetNs + "DistanceMeters").Value));
    }

    [Test]
    [TestCase(60 * 180)]
    public void When_export_should_trackmodel_totaltimeseconds(double value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel() { Duration = TimeSpan.FromSeconds(value) }, null, outStream);

      XDocument actual = Parse(outStream);

      Assert.AreEqual(value, XmlConvert.ToDouble(actual.Root.Activities().First().Laps().First().Element(TargetNs + "TotalTimeSeconds").Value));
    }

    [Test]
    [TestCase(145)]
    public void When_export_should_trackmodel_avgheart(int value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel() { HeartRateAvg = value }, null, outStream);

      XDocument actual = Parse(outStream);

      Assert.AreEqual(value, XmlConvert.ToInt32(actual.Root.Activities().First().Laps().First().Element(TargetNs + "AverageHeartRateBpm").Element(TargetNs + "Value").Value));
    }

    [Test]
    [TestCase(190)]
    public void When_export_should_trackmodel_maxheart(int value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel() { HeartRateMax = value }, null, outStream);

      XDocument actual = Parse(outStream);

      Assert.AreEqual(value, XmlConvert.ToInt32(actual.Root.Activities().First().Laps().First().Element(TargetNs + "MaximumHeartRateBpm").Element(TargetNs + "Value").Value));
    }

    [Test]
    [TestCase(100)]
    public void When_export_should_trackmodel_cadence(int value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel() { CadenceAvg = value }, null, outStream);

      XDocument actual = Parse(outStream);

      Assert.AreEqual(value, XmlConvert.ToInt32(actual.Root.Activities().First().Laps().First().Element(TargetNs + "Cadence").Value));
    }

    [Test]
    [TestCase(5.67, 7.89)]
    public void When_export_should_gpx_trackpoint_lon_lat_correct(double lon, double lat)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Longitude = lon, Latitude = lat } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.Activities().First().Laps().First().Tracks().First().Trackpoints().First();

      Assert.AreEqual(lon, XmlConvert.ToDouble(tp.Element(TargetNs + "Position").Element(TargetNs + "LongitudeDegrees").Value));
      Assert.AreEqual(lat, XmlConvert.ToDouble(tp.Element(TargetNs + "Position").Element(TargetNs + "LatitudeDegrees").Value));
    }

    [Test]
    [TestCase(156)]
    public void When_export_should_trackpoint_heartrate(int value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Heartrate = value } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.Activities().First().Laps().First().Tracks().First().Trackpoints().First();

      Assert.AreEqual(value, XmlConvert.ToInt32(tp.Element(TargetNs + "HeartRateBpm").Element(TargetNs + "Value").Value));
    }

    [Test]
    [TestCase(340)]
    [Ignore]
    public void When_export_should_trackpoint_power(int value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Power = value } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.Activities().First().Laps().First().Tracks().First().Trackpoints().First();

      Assert.AreEqual(value, XmlConvert.ToInt32(tp.Element(TargetNs + "Extensions").Element(TpExtNs + "TrackPointExtension").Element(TpExtNs + "power").Value));
    }

    [Test]
    [TestCase(340)]
    public void When_export_should_trackpoint_power_on_trainingstagebich(int value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Power = value } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.Activities().First().Laps().First().Tracks().First().Trackpoints().First();

      Assert.AreEqual(value, XmlConvert.ToInt32(tp.Element(TargetNs + "Extensions").Element(TpExtNs + "TPX").Element(TpExtNs + "Watts").Value));
    }

    [Test]
    [TestCase(45)]
    public void When_export_should_trackpoint_elevation(int value)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      sut.Export(new TrackModel(), new[] { new TrackPointModel() { Elevation = value } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.Activities().First().Laps().First().Tracks().First().Trackpoints().First();

      Assert.AreEqual(value, XmlConvert.ToDouble(tp.Element(TargetNs + "AltitudeMeters").Value));
    }

    [Test]
    [TestCase("2014-01-18T14:00:00Z", 32.0)]
    public void When_export_should_trackpoint_time(string trackModelDate, double trackPointTime)
    {
      Stream outStream = CreateStream();

      TCXv2ExportService sut = new TCXv2ExportServiceBuilder().Build();
      DateTime baseDate = DateTime.Parse(trackModelDate);
      TimeSpan relTime = TimeSpan.FromSeconds(trackPointTime);

      sut.Export(new TrackModel() { Date = baseDate }, new[] { new TrackPointModel() { Time = relTime } }, outStream);

      XDocument actual = Parse(outStream);

      XElement tp = actual.Root.Activities().First().Laps().First().Tracks().First().Trackpoints().First();

      Assert.AreEqual((baseDate + relTime).ToUniversalTime(), DateTime.Parse(tp.Element(TargetNs + "Time").Value).ToUniversalTime());
    }

    private XDocument Parse(Stream outStream)
    {
      outStream.Position = 0;
      return XDocument.Load(outStream);
    }

    private Stream CreateStream()
    {
      return new MemoryStream();
    }
  }

  public class TCXv2ExportServiceBuilder
  {
    public TCXv2ExportService Build()
    {
      return new TCXv2ExportService();
    }
  }
}
