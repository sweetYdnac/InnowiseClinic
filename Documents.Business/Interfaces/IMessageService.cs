using Shared.Messages;

namespace Documents.Business.Interfaces
{
    public interface IMessageService
    {
        Task SendAddLogMessageAsync(AddLogMessage message);
    }
}

