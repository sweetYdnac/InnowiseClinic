using Services.Data.Contexts;
using Services.Data.Entities;
using Services.Data.Interfaces;

namespace Services.Data.Implementations
{
    public class SpecializationRepository : IGenericRepository<Specialization>
    {
        private readonly ServicesDbContext _db;

        public SpecializationRepository(ServicesDbContext db) => _db = db;

        public async Task AddAsync(Specialization entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
    }
}
