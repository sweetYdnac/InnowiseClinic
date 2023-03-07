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

        public async Task UpdateDoctorAsync(Guid id, string specializationName)
        {
            await DbSet
                .Include(a => a.Appointment)
                .Where(a => a.Appointment.DoctorId.Equals(id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.DoctorSpecializationName, a => specializationName));
        }

        public async Task UpdatePatientAsync(Guid id, DateOnly dateOfBirth)
        {
            await DbSet
                .Include(a => a.Appointment)
                .Where(a => a.Appointment.PatientId.Equals(id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.PatientDateOfBirth, a => dateOfBirth));
        }
    }
}
