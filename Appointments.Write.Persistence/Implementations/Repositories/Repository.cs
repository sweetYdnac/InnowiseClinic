using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Domain.Common;
using Appointments.Write.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Write.Persistence.Implementations.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected AppointmentsDbContext Database { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public Repository(AppointmentsDbContext database) =>
            (Database, DbSet) = (database, database.Set<T>());

        public async Task<int> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            return await Database.SaveChangesAsync();
        }
    }
}
