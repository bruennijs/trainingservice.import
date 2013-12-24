// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VdoTrackRepository.cs" company="Gira, Giersiepen GmbH &amp; Co. KG">
//   Copyright (c) 2010 Gira, Giersiepen GmbH &amp; Co. KG. All rights reserved.
// </copyright>
// <author>ise GmbH</author>
// --------------------------------------------------------------------------------------------------------------------

namespace trainingservice.vdoimport.Repository
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Data.Odbc;
  using System.Globalization;

  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Interfaces.Repository;

  /// <summary />
  public class VdoTrackRepository : ITrackRepository
  {
    private readonly string connectionString;

    private readonly DateTime durationBaseDate = DateTime.Parse("30.12.1899 00:00:00");

    /// <summary>
    /// Initializes a new instance of the <see cref="VdoTrackRepository"/> class.
    /// </summary>
    public VdoTrackRepository(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public IEnumerable<TrackModel> GetTracks()
    {
      using (var connection = this.CreateDbConnection())
      {
        connection.Open();

        using (IDbCommand cmd = this.CreateSelectDatenCommand(connection))
        {
          using (IDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              yield return
                new TrackModel()
                  {
                    Id = reader.GetInt32(0).ToString(CultureInfo.InvariantCulture),
                    Date = reader.GetDateTime(1),
                    Duration = this.CalcualteDuration(reader.GetDateTime(2)),
                    Elavation = Convert.ToUInt32(reader.GetDouble(4)),
                    Distance = Convert.ToUInt32(reader.GetDouble(3)),
                    HeartRateAvg = Convert.ToUInt32(reader.GetDouble(5)),
                    HeartRateMax = Convert.ToUInt32(reader.GetDouble(6))
                  };
            }
          }
        }
      }
    }

    private TimeSpan CalcualteDuration(DateTime dateTime)
    {
      return dateTime - durationBaseDate;
    }

    private IDbCommand CreateSelectDatenCommand(IDbConnection connection)
    {
      IDbCommand cmd = connection.CreateCommand();
      cmd.CommandText = "SELECT dat_Id, Datum, Zeit, meter, Hoehendifferenz, Puls, Maxpuls, Startzeit from daten";
      return cmd;
    }

    private IDbConnection CreateDbConnection()
    {
      return new OdbcConnection(this.connectionString);
    }
  }
}
