﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionsRepository
    {
        IEnumerable<Region> GetAll();
    }
}
