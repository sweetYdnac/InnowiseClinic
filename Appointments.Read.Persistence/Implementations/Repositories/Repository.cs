using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Common;
using Appointments.Read.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System.Linq.Expressions;

namespace Appointments.Read.Persistence.Implementations.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppointmentsDbContext Database;
        protected readonly DbSet<T> DbSet;

        public Repository(AppointmentsDbContext database) => (Database, DbSet) = (database, database.Set<T>());

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

        public async Task<int> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
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
