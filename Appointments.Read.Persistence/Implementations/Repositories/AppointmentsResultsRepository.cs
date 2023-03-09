using Appointments.Read.Application.DTOs.AppointmentResult;
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

        public async Task<AppointmentResultDTO> GetByIdAsync(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(r => r.Appointment)
                .Select(r => new AppointmentResultDTO
                {
                    Id = r.Id,
                    Date = r.Date,
                    PatientFullName = r.Appointment.PatientFullName,
                    PatientDateOfBirth = r.Appointment.PatientDateOfBirth,
                    DoctorId = r.Appointment.DoctorId,
                    DoctorFullName = r.Appointment.DoctorFullName,
                    DoctorSpecializationName = r.Appointment.DoctorSpecializationName,
                    ServiceName = r.Appointment.ServiceName,
                    Complaints = r.Complaints,
                    Conclusion = r.Conclusion,
                    Recomendations = r.Recomendations,
                })
                .FirstOrDefaultAsync(r => r.Id.Equals(id));
        }

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
