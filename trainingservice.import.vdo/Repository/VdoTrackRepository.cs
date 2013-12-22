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
  using System.Data.OleDb;
  using System.Globalization;

  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Interfaces.Repository;

  /// <summary />
  public class VdoTrackRepository : ITrackRepository
  {
    private readonly string connectionString;

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
                    Id = reader.GetString(0),
                    Date = reader.GetDateTime(1),
                    Duration = TimeSpan.Parse(reader.GetString(3), CultureInfo.InvariantCulture),
                    Elavation = Convert.ToUInt32(reader.GetInt32(5)),
                    Distance = Convert.ToUInt32(reader.GetInt32(4))
                  };
            }
          }
        }
      }
    }

    private IDbCommand CreateSelectDatenCommand(IDbConnection connection)
    {
      IDbCommand cmd = connection.CreateCommand();
      cmd.CommandText = "SELECT dat_Id, Datum, Startzeit, Zeit, meter, Hoehendifferenz, Puls, Maxpuls from daten;";
      return cmd;
    }

    private IDbConnection CreateDbConnection()
    {
      return new OleDbConnection(this.connectionString);
    }
  }
}
