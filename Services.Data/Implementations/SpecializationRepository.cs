using Microsoft.EntityFrameworkCore;
using Services.Data.Contexts;
using Services.Data.Entities;
using Services.Data.Interfaces;

namespace Services.Data.Implementations
{
    public class SpecializationRepository : IRepository<Specialization>
    {
        private readonly ServicesDbContext _db;

        public SpecializationRepository(ServicesDbContext db) => _db = db;

        public async Task AddAsync(Specialization entity)
        {
            await _db.Specializations.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Specialization> GetByIdAsync(Guid id)
        {
            return await _db.Specializations.FirstOrDefaultAsync(s => s.Id.Equals(id));
        }
    }
}
