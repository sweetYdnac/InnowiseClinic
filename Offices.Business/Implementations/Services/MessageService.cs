using MassTransit;
using Offices.Business.Interfaces.Services;
using Shared.Messages;

namespace Offices.Business.Implementations.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageService(ISendEndpointProvider sendEndpointProvider) => _sendEndpointProvider = sendEndpointProvider;

        public async Task SendOfficeDisabledMessageAsync(Guid officeId)
        {
            await _sendEndpointProvider.Send(new OfficeDisabledMessage { OfficeId = officeId });
        }

        public async Task SendOfficeUpdatedMessageAsync(Guid officeId, string city, string street, string houseNumber, string officeNumber, bool isActive)
        {
            var message = new OfficeUpdatedMessage
            {
                OfficeId = officeId,
                OfficeAddress = $"{city} {street} {houseNumber} {officeNumber}",
                IsActive = isActive,
            };

            await _sendEndpointProvider.Send(message);
        }
    }
}
