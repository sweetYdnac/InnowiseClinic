using Documents.Business.Interfaces;
using MassTransit;
using Shared.Messages;

namespace Documents.Business.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageService(ISendEndpointProvider sendEndpointProvider) => _sendEndpointProvider = sendEndpointProvider;

        public async Task SendAddLogMessageAsync(AddLogMessage message) =>
            await _sendEndpointProvider.Send(message);
    }
}
