using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZ_Walks_DB_Context _dbContext;
        public SQLRegionRepository(NZ_Walks_DB_Context dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }
    }
}