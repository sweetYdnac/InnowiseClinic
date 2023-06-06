using Shared.Messages;

namespace Appointments.Read.Application.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendAddLogMessageAsync(AddLogMessage message);
    }
}

