// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackRepositoryTest.cs" company="Gira, Giersiepen GmbH &amp; Co. KG">
//   Copyright (c) 2010 Gira, Giersiepen GmbH &amp; Co. KG. All rights reserved.
// </copyright>
// <author>ise GmbH</author>
// --------------------------------------------------------------------------------------------------------------------

namespace trainingservice.import.vdo.test
{
  using System;
  using System.Linq;

  using NUnit.Framework;

  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.import.vdo.test.Builder;

  [TestFixture]
  public class VdoTrackRepositoryTest
  {
    [Test]
    [TestCase(@"C:\git\trainingservice\trainingservice.import.vdo.test\TestFiles\vdo1.pcs", 7)]
    public void When_get_tracks_should_return_expected_track_count(string filePath, int count)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(count, sut.GetTracks().Count());
    }

    [Test]
    [TestCase(@"C:\git\trainingservice\trainingservice.import.vdo.test\TestFiles\vdo1.pcs", "4", "2013-12-07T14:10:00Z")]
    public void When_get_tracks_should_return_expected_track_count(string filePath, string id, string time)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(DateTime.Parse(time).ToUniversalTime(), sut.GetTracks().Single(t => t.Id == id).Date);
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "1", 0.0)]
    [TestCase(@".\TestFiles\vdo1.pcs", "5", 51.0)]
    public void When_get_duration_should_return_expected_value(string filePath, string id, double duration)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(TimeSpan.FromMinutes(duration), sut.GetTracks().Single(tm => tm.Id.Equals(id)).Duration);
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "1", 113624.0)]
    [TestCase(@".\TestFiles\vdo1.pcs", "5", 21312.0)]
    public void When_get_distance_should_return_expected_value(string filePath, string id, double value)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(value, sut.GetTracks().Single(tm => tm.Id.Equals(id)).Distance);
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "3", 246)]
    public void When_get_trackpoints_of_trackid_return_expected_couhnt_of_models(string filePath, string id, int count)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(count, sut.GetTrackPoints(sut.GetTracks().Single(t => t.Id == id)).Count());
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "3", "11111", 30890)]
    public void When_get_trackpoints_expect_meter(string filePath, string id, string trackPointId, int meter)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(meter, sut.GetTrackPoints(sut.GetTracks().Single(t => t.Id == id)).Single(tp => tp.Id == trackPointId).Distance);
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "3", "11111", 117)]
    public void When_get_trackpoints_expect_heartrate(string filePath, string id, string trackPointId, int value)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(value, sut.GetTrackPoints(sut.GetTracks().Single(t => t.Id == id)).Single(tp => tp.Id == trackPointId).Heartrate);
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "3", "11111", 4920.0)]
    public void When_get_trackpoints_expect_time(string filePath, string id, string trackPointId, double time)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(TimeSpan.FromSeconds(time), sut.GetTrackPoints(sut.GetTracks().Single(t => t.Id == id)).Single(tp => tp.Id == trackPointId).Time);
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "3", "11111", -80)]
    public void When_get_trackpoints_expect_elevation(string filePath, string id, string trackPointId, int value)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(value, sut.GetTrackPoints(sut.GetTracks().Single(t => t.Id == id)).Single(tp => tp.Id == trackPointId).Elevation);
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "3", "11110", 27)]
    public void When_get_trackpoints_expect_power(string filePath, string id, string trackPointId, int value)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(value, sut.GetTrackPoints(sut.GetTracks().Single(t => t.Id == id)).Single(tp => tp.Id == trackPointId).Power);
    }

    [Test]
    [TestCase(@".\TestFiles\vdo1.pcs", "2", "9950", 51.85244, 10.27483)]
    public void When_get_trackpoints_expect_lon_lat(string filePath, string id, string trackPointId, double lat, double lon)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      TrackPointModel tpModel = sut.GetTrackPoints(sut.GetTracks().Single(t => t.Id == id)).Single(tp => tp.Id == trackPointId);
      Assert.AreEqual(lon, tpModel.Longitude);
      Assert.AreEqual(lat, tpModel.Latitude);
    }
  }
}
