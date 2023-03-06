using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using Appointments.Read.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Read.Persistence.Implementations.Repositories
{
    public class AppointmentsRepository : Repository<Appointment>, IAppointmentsRepository
    {
        public AppointmentsRepository(AppointmentsDbContext database)
            : base(database) { }

        public async Task UpdatePatientAsync(Guid id, string fullName, string phoneNumber)
        {
            await DbSet
                .Where(a => a.Id.Equals(id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.PatientFullName, a => fullName)
                    .SetProperty(a => a.PatientPhoneNumber, a => phoneNumber));
        }
    }
}
