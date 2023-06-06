using Shared.Messages;

namespace Appointments.Write.Application.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendCreateAppointmentMessageAsync(CreateAppointmentMessage message);
        Task SendRescheduleAppointmentMessageAsync(RescheduleAppointmentMessage message);
        Task SendCreateAppointmentResultMessageAsync(CreateAppointmentResultMessage message);
        Task SendEditAppointmentResultMessageAsync(EditAppointmentResultMessage message);
        Task SendDeleteAppointmentMessageAsync(Guid id);
        Task SendApproveAppointmentMessageAsync(Guid id);
        Task SendGeneratePdfMessageAsync(GeneratePdfMessage message);
        Task SendAddLogMessageAsync(AddLogMessage message);
    }
}
