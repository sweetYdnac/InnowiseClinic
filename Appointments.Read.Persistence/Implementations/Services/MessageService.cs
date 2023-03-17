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
                Bytes = System.Text.Encoding.Default.GetString(content),
            };

            await _sendEndpointProvider.Send(message);
        }
    }
}
