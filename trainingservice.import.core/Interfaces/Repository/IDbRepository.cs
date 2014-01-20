// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITrainingImportService.cs" company="Gira, Giersiepen GmbH &amp; Co. KG">
//   Copyright (c) 2010 Gira, Giersiepen GmbH &amp; Co. KG. All rights reserved.
// </copyright>
// <author>ise GmbH</author>
// --------------------------------------------------------------------------------------------------------------------

namespace trainingservice.import.core.Interfaces.Repository
{
  using System.Collections.Generic;
  using System.IO;

  using trainingservice.import.core.Interfaces.Models;

  /// <summary>
  /// The TrainingImportService interface.
  /// </summary>
  public interface IDbRepository
  {
    /// <summary>
    /// Imports the specified database file.
    /// </summary>
    /// <param name="dbFile">The database file.</param>
    /// <returns>Model representing a imported db.</returns>
    DbModel Create(Stream dbFile);

    /// <summary>
    /// Deletes a db.
    /// </summary>
    /// <param name="id"></param>
    void Delete(string id);

    /// <summary>
    /// Gets the by identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// Db model
    /// </returns>
    DbModel GetById(string id);

    /// <summary>
    /// Gets all db models existing.
    /// </summary>
    /// <returns>DB models of this db.</returns>
    IEnumerable<DbModel> Get();
  }
}
