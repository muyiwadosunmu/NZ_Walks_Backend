using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }
}