using Appointments.Write.Application.Features.Commands.Appointments;
using Appointments.Write.Domain.Entities;

namespace Appointments.Write.Application.Interfaces.Repositories
{
    public interface IAppointmentsRepository : IRepository<Appointment>
    {
        Task<int> RescheduleAppointment(RescheduleAppointmentCommand command);
        Task<int> ApproveAsync(Guid id);
    }
}
