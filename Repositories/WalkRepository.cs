using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}