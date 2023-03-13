using Appointments.Write.Application.DTOs.AppointmentResult;
using Appointments.Write.Domain.Entities;

namespace Appointments.Write.Application.Interfaces.Repositories
{
    public interface IAppointmentsResultsRepository : IRepository<AppointmentResult>
    {
        Task<int> UpdateAsync(EditAppointmentResultDTO dto);
    }
}
