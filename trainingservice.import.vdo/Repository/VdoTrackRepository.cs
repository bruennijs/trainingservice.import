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
  using System.Xml.Linq;

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

    public IEnumerable<TrackPointModel> GetTrackPoints(TrackModel track)
    {
      using (var connection = this.CreateDbConnection())
      {
        connection.Open();

        using (IDbCommand cmd = this.CreateSelectZeitCommand(connection, track))
        {
          using (IDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              yield return ParseTrackPoint(reader);
            }
          }
        }
      }
    }

    private static TrackPointModel ParseTrackPoint(IDataReader reader)
    {
      ////DateTime time = reader.GetDateTime(2);      

      return new TrackPointModel()
               {
                 Id = reader.GetInt32(0).ToString(CultureInfo.InvariantCulture),
                 Distance = reader.GetInt32(1),
                 Time = DateTime.Parse(reader.GetString(2), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime(),
                 Heartrate = reader.GetInt32(3),
                 Elevation = reader.GetInt32(4),
                 Cadence = reader.GetInt32(5),
                 Latitude = reader.GetDouble(6),
                 Longitude = reader.GetDouble(7),
                 Power = reader.GetInt32(8)
               };
    }

    private TimeSpan CalcualteDuration(DateTime dateTime)
    {
      return dateTime - durationBaseDate;
    }

    private IDbCommand CreateSelectZeitCommand(IDbConnection connection, TrackModel model)
    {
      IDbCommand cmd = connection.CreateCommand();
      cmd.CommandText =
        string.Format(
          "SELECT zei_primaryKey, meter, Zwischenzeit, Puls, HDiff, zei_trittfrequenz, zei_lat, zei_lon, zei_watt from zeit where (zei_Id = {0} AND meter <> 0)",
          model.Id);
      return cmd;
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
