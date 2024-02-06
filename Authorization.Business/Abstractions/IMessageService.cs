using Shared.Messages;

namespace Authorization.Business.Abstractions
{
    public interface IMessageService
    {
        Task SendAddLogMessageAsync(AddLogMessage message);
    }
}

