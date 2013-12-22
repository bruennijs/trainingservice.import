// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackRepositoryTest.cs" company="Gira, Giersiepen GmbH &amp; Co. KG">
//   Copyright (c) 2010 Gira, Giersiepen GmbH &amp; Co. KG. All rights reserved.
// </copyright>
// <author>ise GmbH</author>
// --------------------------------------------------------------------------------------------------------------------

namespace trainingservice.import.vdo.test
{
  using System.Linq;

  using NUnit.Framework;

  using trainingservice.import.core.Interfaces.Repository;
  using trainingservice.import.vdo.test.Builder;

  [TestFixture]
  public class TrackRepositoryTest
  {
    [Test]
    [TestCase("TestFiles\\vdo1.mdb", 7)]
    public void When_get_tracks_should_return_expected_track_count(string filePath, int count)
    {
      ITrackRepository sut = new VdoTrackRepositoryBuilder().WithDatabaseFilePath(filePath).Build();
      Assert.AreEqual(count, sut.GetTracks().Count());
    }
  }
}
