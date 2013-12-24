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
  }
}
