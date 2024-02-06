using Authorization.Business.Abstractions;
using MassTransit;
using Shared.Messages;

namespace Authorization.Business.ServicesImplementations
{
    public class MessageService : IMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageService(ISendEndpointProvider sendEndpointProvider) => _sendEndpointProvider = sendEndpointProvider;

        public async Task SendAddLogMessageAsync(AddLogMessage message) =>
            await _sendEndpointProvider.Send(message);
    }
}
