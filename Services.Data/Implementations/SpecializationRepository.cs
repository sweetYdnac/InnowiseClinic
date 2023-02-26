using Microsoft.EntityFrameworkCore;
using Services.Data.Contexts;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Models;

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

        public async Task<PagedResult<Specialization>> GetPagedAndFilteredAsync(int currentPage, int pageSize, params Func<Specialization, bool>[] filters)
        {
            var response = _db.Specializations.AsQueryable();

            foreach (var filter in filters)
            {
                response.Where(filter);
            }

            var items = await response
                .Skip(pageSize * (currentPage - 1))
                .Take(pageSize)
                .ToArrayAsync();

            var totalCount = await response.CountAsync();

            return new() 
            {  
                Items = items, 
                TotalCount = totalCount 
            };
        }
    }
}
