using Microsoft.EntityFrameworkCore;
using Services.Data.Contexts;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Models;
using System.Linq.Expressions;

namespace Services.Data.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ServicesDbContext Database;
        protected readonly DbSet<T> DbSet;

        public Repository(ServicesDbContext database)
        {
            Database = database;
            DbSet = database.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, params Expression<Func<T, bool>>[] filters)
        {
            var response = DbSet
                .AsNoTracking()
                .AsQueryable();

            foreach (var filter in filters)
            {
                response = response.Where(filter);
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

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await Database.SaveChangesAsync();
        }

        public async Task ChangeStatusAsync(Guid id, bool isActive)
        {
            await DbSet
                .Where(e => e.Id.Equals(id))
                .ExecuteUpdateAsync(e => e.SetProperty(n => n.IsActive, n => isActive));
        }
    }
}
