namespace trainingservice.vdoimport.Repository
{
  using System;
  using System.Collections.Generic;
  using System.IO;

  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Interfaces.Repository;

  public class VdoDbRepository : IDbRepository
  {
    public DbModel Create(Stream dbFile)
    {
      throw new NotImplementedException();
    }

    public void Delete(string id)
    {
      throw new NotImplementedException();
    }

    public DbModel GetById(string id)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<DbModel> Get()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<TrackSample> GetTrackSamples(string dbId, string trackId)
    {
      throw new NotImplementedException();
    }
  }
}