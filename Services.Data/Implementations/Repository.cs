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

        public Repository(ServicesDbContext database) => (Database, DbSet) = (database, database.Set<T>());

        public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            var query = DbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, params Expression<Func<T, bool>>[] filters)
        {
            var query = DbSet.AsNoTracking();

            return await GetPagedAndFilteredAsync(query, currentPage, pageSize, filters);
        }

        public async Task<PagedResult<T>> GetPagedAndFilteredAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<T, object>>> includes, params Expression<Func<T, bool>>[] filters)
        {
            var query = DbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await GetPagedAndFilteredAsync(query, currentPage, pageSize, filters);
        }

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await Database.SaveChangesAsync();
        }

        public async Task ChangeStatusAsync(Guid id, bool isActive) =>
            await DbSet
                .Where(e => e.Id.Equals(id))
                .ExecuteUpdateAsync(e => e.SetProperty(n => n.IsActive, n => isActive));

        public async Task<int> UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            return await Database.SaveChangesAsync();
        }

        private static async Task<PagedResult<T>> GetPagedAndFilteredAsync(IQueryable<T> query, int currentPage, int pageSize, params Expression<Func<T, bool>>[] filters)
        {
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            var items = await query
                .Skip(pageSize * (currentPage - 1))
                .Take(pageSize)
                .ToArrayAsync();

            var totalCount = await query.CountAsync();

            return new()
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
