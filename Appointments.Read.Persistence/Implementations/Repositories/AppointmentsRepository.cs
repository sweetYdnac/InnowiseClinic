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

        public async Task<int> UpdateDoctorAsync(Guid id, string fullName)
        {
            return await DbSet
                .Where(a => a.DoctorId.Equals(id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.DoctorFullName, a => fullName));
        }

        public async Task<int> UpdatePatientAsync(Guid id, string fullName, string phoneNumber)
        {
            return await DbSet
                .Where(a => a.PatientId.Equals(id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.PatientFullName, a => fullName)
                    .SetProperty(a => a.PatientPhoneNumber, a => phoneNumber));
        }

        public async Task<int> UpdateServiceAsync(Guid id, string name, int timeSlotSize)
        {
            return await DbSet
                .Where(a => a.ServiceId.Equals(id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.ServiceName, a => name)
                    .SetProperty(a => a.Duration, a => timeSlotSize));
        }

        public async Task<int> RescheduleAsync(Guid id, Guid doctorId, DateOnly date, TimeOnly time, string doctorFullName)
        {
            return await DbSet
                .Where(a => a.Id.Equals(id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.DoctorId, a => doctorId)
                    .SetProperty(a => a.Date, a => date)
                    .SetProperty(a => a.Time, a => time)
                    .SetProperty(a => a.DoctorFullName, a => doctorFullName));
        }

        public async Task<int> DeleteByIdAsync(Guid id)
        {
            return await DbSet
                .Where(a => a.Id.Equals(id))
                .ExecuteDeleteAsync();
        }

        public async Task<int> ApproveAsync(Guid id)
        {
            return await DbSet
                .Where(a => a.Id.Equals(id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.IsApproved, a => true));
        }
    }
}
