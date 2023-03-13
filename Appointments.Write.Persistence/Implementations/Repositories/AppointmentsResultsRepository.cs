using Appointments.Write.Application.DTOs.AppointmentResult;
using Appointments.Write.Application.Interfaces.Repositories;
using Appointments.Write.Domain.Entities;
using Appointments.Write.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Write.Persistence.Implementations.Repositories
{
    public class AppointmentsResultsRepository : Repository<AppointmentResult>, IAppointmentsResultsRepository
    {
        public AppointmentsResultsRepository(AppointmentsDbContext database)
            : base(database) { }

        public async Task<int> UpdateAsync(EditAppointmentResultDTO dto)
        {
            return await DbSet
                .Where(r => r.Id.Equals(dto.Id))
                .ExecuteUpdateAsync(p => p
                .SetProperty(a => a.Complaints, a => dto.Complaints)
                .SetProperty(a => a.Conclusion, a => dto.Conclusion)
                .SetProperty(a => a.Recomendations, a => dto.Recomendations));
        }
    }
}
