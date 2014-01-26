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

    private static readonly DateTime durationBaseDate = DateTime.Parse("30.12.1899 00:00:00");

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
              yield return this.ParseTrackModel(reader);
            }
          }
        }
      }
    }

    private TrackModel ParseTrackModel(IDataReader reader)
    {
      string id = reader.GetInt32(0).ToString(CultureInfo.InvariantCulture);
      DateTime date = reader.GetDateTime(1);
      TimeSpan startTime = CalcualteDuration(reader.GetDateTime(2));
      return new TrackModel()
               {
                 Id = id,
                 Date = date.Add(startTime),
                 Duration = CalcualteDuration(reader.GetDateTime(3)),
                 Elavation = reader.GetInt32(5),
                 Distance = reader.GetDouble(4),
                 HeartRateAvg = reader.GetInt32(6),
                 HeartRateMax = reader.GetInt32(7),
                 ////CadenceAvg = reader.GetDouble(8)
               };
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
      return new TrackPointModel()
               {
                 Id = reader.GetInt32(0).ToString(CultureInfo.InvariantCulture),
                 Distance = reader.GetDouble(1),
                 ////Time = DateTime.Parse(reader.GetString(2), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime(),
                 Time = CalcualteDuration(DateTime.Parse(reader.GetString(2), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime()),
                 Heartrate = reader.GetInt32(3),
                 Elevation = reader.GetInt32(4),
                 Cadence = reader.GetInt32(5),
                 Latitude = reader.GetDouble(6),
                 Longitude = reader.GetDouble(7),
                 Power = reader.GetInt32(8)
               };
    }

    private static TimeSpan CalcualteDuration(DateTime dateTime)
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
      cmd.CommandText = "SELECT dat_Id, Datum, Startzeit, Zeit, meter, Hoehendifferenz, Puls, Maxpuls from daten";
      return cmd;
    }

    private IDbConnection CreateDbConnection()
    {
      return new OdbcConnection(this.connectionString);
    }
  }
}
