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

        public async Task<int> UpdateDoctorAsync(UpdateDoctorDTO dto)
        {
            return await DbSet
                .Where(a => a.DoctorId.Equals(dto.Id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.DoctorFullName, a => dto.FullName)
                    .SetProperty(a => a.DoctorSpecializationName, a => dto.SpecializationName)
                    .SetProperty(a => a.OfficeId, a => dto.OfficeId));
        }

        public async Task<int> UpdatePatientAsync(UpdatePatientDTO dto)
        {
            return await DbSet
                .Where(a => a.PatientId.Equals(dto.Id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.PatientFullName, a => dto.FullName)
                    .SetProperty(a => a.PatientPhoneNumber, a => dto.PhoneNumber)
                    .SetProperty(a => a.PatientDateOfBirth, a => dto.DateOfBirth));
        }

        public async Task<int> UpdateServiceAsync(UpdateServiceDTO dto)
        {
            return await DbSet
                .Where(a => a.ServiceId.Equals(dto.Id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.ServiceName, a => dto.Name)
                    .SetProperty(a => a.Duration, a => dto.TimeSlotSize));
        }

        public async Task<int> RescheduleAsync(RescheduleAppointmentDTO dto)
        {
            return await DbSet
                .Where(a => a.Id.Equals(dto.Id))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(a => a.DoctorId, a => dto.DoctorId)
                    .SetProperty(a => a.OfficeId, a => dto.OfficeId)
                    .SetProperty(a => a.Date, a => dto.Date)
                    .SetProperty(a => a.Time, a => dto.Time)
                    .SetProperty(a => a.DoctorFullName, a => dto.DoctorFullName));
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

        public async Task<IEnumerable<TimeSlotAppointmentDTO>> GetAppointmentsAsync(DateOnly date, Guid serviceId, Guid? doctorId)
        {
            Expression<Func<Appointment, bool>> filter = doctorId is null
                ? a => a.Date.Equals(date) && a.ServiceId.Equals(serviceId)
                : a => a.Date.Equals(date) && a.DoctorId.Equals(doctorId);

            return await DbSet
                .AsNoTracking()
                .Where(filter)
                .Select(a => new TimeSlotAppointmentDTO
                {
                    StartTime = a.Time,
                    EndTime = a.Time.AddMinutes(a.Duration),
                    DoctorId = a.DoctorId,
                })
                .ToArrayAsync();
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
                .FilterMany(filters);

            var totalCount = await query.CountAsync();

            var items = totalCount > 0
                ? await query
                    .FilterByPage(currentPage, pageSize)
                    .SortMany(sorts)
                    .Select(a => new DoctorScheduledAppointmentDTO
                    {
                        Id = a.Id,
                        StartTime = a.Time,
                        EndTime = a.Time.AddMinutes(a.Duration),
                        PatientId = a.PatientId,
                        PatientFullName = a.PatientFullName,
                        PatientDateOfBirth = a.PatientDateOfBirth,
                        DoctorFullName = a.DoctorFullName,
                        DoctorSpecializationName = a.DoctorSpecializationName,
                        ServiceName = a.ServiceName,
                        IsApproved = a.IsApproved,
                        ResultId = a.AppointmentResult == null ? null : a.AppointmentResult.Id,
                    })
                    .ToArrayAsync()
                : Enumerable.Empty<DoctorScheduledAppointmentDTO>();

            return new()
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResult<AppointmentDTO>> GetAppointmentsAsync(int currentPage, int pageSize, params Expression<Func<Appointment, bool>>[] filters)
        {
            return await GetAppointmentsAsync(currentPage, pageSize, null, filters);
        }

        public async Task<PagedResult<AppointmentDTO>> GetAppointmentsAsync(int currentPage, int pageSize, IDictionary<Expression<Func<Appointment, object>>, bool> sorts = null, params Expression<Func<Appointment, bool>>[] filters)
        {
            var query = DbSet
                .AsNoTracking()
                .FilterMany(filters);

            var totalCount = await query.CountAsync();

            var items = totalCount > 0
                ? await query
                    .FilterByPage(currentPage, pageSize)
                    .SortMany(sorts)
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
                    .ToArrayAsync()
                : Enumerable.Empty<AppointmentDTO>();

            return new()
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResult<AppointmentHistoryDTO>> GetAppointmentHistoryAsync(int currentPage, int pageSize, params Expression<Func<Appointment, bool>>[] filters)
        {
            return await GetAppointmentHistoryAsync(currentPage, pageSize, null, filters);
        }

        public Task<PagedResult<AppointmentHistoryDTO>> GetAppointmentHistoryAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<Appointment, object>>> includes, params Expression<Func<Appointment, bool>>[] filters)
        {
            return GetAppointmentHistoryAsync(currentPage, pageSize, includes, null, filters);
        }

        public async Task<PagedResult<AppointmentHistoryDTO>> GetAppointmentHistoryAsync(int currentPage, int pageSize, IEnumerable<Expression<Func<Appointment, object>>> includes, IDictionary<Expression<Func<Appointment, object>>, bool> sorts = null, params Expression<Func<Appointment, bool>>[] filters)
        {
            var query = DbSet
                .AsNoTracking()
                .IncludeMany(includes)
                .FilterMany(filters);

            var totalCount = await query.CountAsync();

            var items = totalCount > 0
                ? await query
                    .FilterByPage(currentPage, pageSize)
                    .SortMany(sorts)
                    .Select(a => new AppointmentHistoryDTO
                    {
                        Date = a.Date,
                        StartTime = a.Time,
                        EndTime = a.Time.AddMinutes(a.Duration),
                        DoctorFullName = a.DoctorFullName,
                        ServiceName = a.ServiceName,
                        ResultId = a.AppointmentResult.Id,
                    })
                    .ToArrayAsync()
                : Enumerable.Empty<AppointmentHistoryDTO>();

            return new()
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
