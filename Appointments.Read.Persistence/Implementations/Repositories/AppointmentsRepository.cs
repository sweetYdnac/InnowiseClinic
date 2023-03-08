using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Domain.Entities;
using Appointments.Read.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Models.Extensions;
using System.Linq.Expressions;

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

        public async Task<PagedResult<DoctorScheduledAppointmentDTO>> GetDoctorScheduleAsync(int currentPage, int pageSize, params Expression<Func<Appointment, bool>>[] filters)
        {
            return await GetDoctorScheduleAsync(currentPage, pageSize, null, filters);
        }

        public async Task<PagedResult<DoctorScheduledAppointmentDTO>> GetDoctorScheduleAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<Appointment, object>>> includes, params Expression<Func<Appointment, bool>>[] filters)
        {
            return await GetDoctorScheduleAsync(currentPage, pageSize, includes, null, filters);
        }

        public async Task<PagedResult<DoctorScheduledAppointmentDTO>> GetDoctorScheduleAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<Appointment, object>>> includes, IDictionary<Expression<Func<Appointment, object>>, bool> sorts, params Expression<Func<Appointment, bool>>[] filters)
        {
            var query = DbSet
                .AsNoTracking()
                .IncludeMany(includes)
                .FilterMany(filters)
                .FilterByPage(currentPage, pageSize)
                .SortMany(sorts);

            var items = await query
                .Select(a => new DoctorScheduledAppointmentDTO
                {
                    StartTime = a.Time,
                    EndTime = a.Time.AddMinutes(a.Duration),
                    PatientId = a.PatientId,
                    PatientFullName = a.PatientFullName,
                    ServiceName = a.ServiceName,
                    IsApproved = a.IsApproved,
                    ResultId = a.AppointmentResult == null ? null : a.AppointmentResult.Id,
                })
                .ToArrayAsync();

            var totalCount = await query.CountAsync();

            return new()
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResult<AppointmentDTO>> GetAppointments(int currentPage, int pageSize, params Expression<Func<Appointment, bool>>[] filters)
        {
            return await GetAppointments(currentPage, pageSize, null, filters);
        }

        public async Task<PagedResult<AppointmentDTO>> GetAppointments(int currentPage, int pageSize, IDictionary<Expression<Func<Appointment, object>>, bool> sorts = null, params Expression<Func<Appointment, bool>>[] filters)
        {
            var query = DbSet
                .AsNoTracking()
                .FilterMany(filters)
                .FilterByPage(currentPage, pageSize)
                .SortMany(sorts);

            var items = await query
                .Select(a => new AppointmentDTO
                {
                    StartTime = a.Time,
                    EndTime = a.Time.AddMinutes(a.Duration),
                    PatientFullName = a.PatientFullName,
                    PatientPhoneNumber = a.PatientPhoneNumber,
                    DoctorFullName = a.DoctorFullName,
                    ServiceName = a.ServiceName,
                    IsApproved = a.IsApproved,
                })
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
