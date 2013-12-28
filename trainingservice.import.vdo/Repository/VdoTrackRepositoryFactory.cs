// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VdoTrackRepositoryFactory.cs" company="Gira, Giersiepen GmbH &amp; Co. KG">
//   Copyright (c) 2010 Gira, Giersiepen GmbH &amp; Co. KG. All rights reserved.
// </copyright>
// <author>ise GmbH</author>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace trainingservice.import.core.Interfaces.Repository
{
  public class VdoTrackRepositoryFactory : ITrackRepositoryFactory
  {
    /// <summary />
    private readonly Func<string, ITrackRepository> repositoryCreator;

    /// <summary>
    /// Initializes a new instance of the <see cref="VdoTrackRepositoryFactory"/> class.
    /// </summary>
    /// <param name="repositoryCreator">
    /// The repository creator.
    /// </param>
    public VdoTrackRepositoryFactory(Func<string, ITrackRepository> repositoryCreator)
    {
      this.repositoryCreator = repositoryCreator;
    }

    /// <summary />
    /// <param name="filePath"></param>
    /// <returns></returns>
    public ITrackRepository Create(string filePath)
    {
      return this.repositoryCreator("Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=" + filePath);
    }
  }
}
