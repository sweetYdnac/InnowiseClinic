using Appointments.Application.Interfaces.Repositories;
using Appointments.Domain.Common;
using Appointments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Persistence.Implementations.Repositories
{
    public class WriteDbRepository<T> : ICommandsRepository<T> where T : BaseEntity
    {
        protected WriteAppointmentsDbContext Database { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public WriteDbRepository(WriteAppointmentsDbContext database) =>
            (Database, DbSet) = (database, Database.Set<T>());

        public async Task<int> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            return await Database.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(T entity) => throw new NotImplementedException();
    }
}
