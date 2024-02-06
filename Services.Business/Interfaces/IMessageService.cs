using Shared.Messages;

namespace Services.Business.Interfaces
{
    public interface IMessageService
    {
        Task SendDisableSpecializationMessageAsync(Guid specializationId);
        Task SendUpdateServiceMessageAsync(Guid id, string name, int timeSlotSize);
        Task SendAddLogMessageAsync(AddLogMessage message);
    }
}
