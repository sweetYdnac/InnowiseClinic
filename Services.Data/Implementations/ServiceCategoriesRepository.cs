using Microsoft.EntityFrameworkCore;
using Services.Data.Contexts;
using Services.Data.Entities;
using Services.Data.Interfaces;

namespace Services.Data.Implementations
{
    public class ServiceCategoriesRepository : IServiceCategoriesRepository
    {
        private readonly ServicesDbContext _db;

        public ServiceCategoriesRepository(ServicesDbContext db) => _db = db;

        public async Task<ServiceCategory> GetByIdAsync(Guid id)
        {
            return await _db.ServiceCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<IEnumerable<ServiceCategory>> GetAllAsync() =>
            await _db.ServiceCategories
                .AsNoTracking()
                .ToArrayAsync();
    }
}
