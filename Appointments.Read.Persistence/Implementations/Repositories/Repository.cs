using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Common;
using Appointments.Read.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Read.Persistence.Implementations.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppointmentsDbContext Database;
        protected readonly DbSet<T> DbSet;

        public Repository(AppointmentsDbContext database) => (Database, DbSet) = (database, database.Set<T>());

        public async Task<int> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            return await Database.SaveChangesAsync();
        }
    }
}
