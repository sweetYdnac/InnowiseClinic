using Appointments.Read.Application.Interfaces.Services;
using MassTransit;
using Shared.Messages;

namespace Appointments.Read.Persistence.Implementations.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageService(ISendEndpointProvider sendEndpointProvider) =>
            _sendEndpointProvider = sendEndpointProvider;

        public async Task SendGeneratePdfAsync(Guid id, byte[] content)
        {
            var message = new GeneratePdfMessage
            {
                Id = id,
                Content = Convert.ToBase64String(content),
            };

            await _sendEndpointProvider.Send(message);
        }
    }
}
