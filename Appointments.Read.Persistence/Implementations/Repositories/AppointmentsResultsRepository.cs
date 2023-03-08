using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using Appointments.Read.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Read.Persistence.Implementations.Repositories
{
    public class AppointmentsResultsRepository : Repository<AppointmentResult>, IAppointmentsResultsRepository
    {
        public AppointmentsResultsRepository(AppointmentsDbContext database)
            : base(database) { }

        public async Task<int> UpdateAsync(Guid id, string complaints, string conclusion, string recomendations)
        {
            return await DbSet
                .Where(r => r.Id.Equals(id))
                .ExecuteUpdateAsync(p => p
                .SetProperty(a => a.Complaints, a => complaints)
                .SetProperty(a => a.Conclusion, a => conclusion)
                .SetProperty(a => a.Recomendations, a => recomendations));
        }
    }
}
