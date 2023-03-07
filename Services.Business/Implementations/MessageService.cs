using MassTransit;
using Services.Business.Interfaces;
using Shared.Messages;

namespace Services.Business.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageService(ISendEndpointProvider sendEndpointProvider) => _sendEndpointProvider = sendEndpointProvider;

        public async Task SendDisableSpecializationMessageAsync(Guid specializationId) =>
            await _sendEndpointProvider.Send(new DisableSpecializationMessage()
            {
                SpecializationId = specializationId,
            });
        public async Task SendUpdateServiceMessageAsync(Guid id, string name, int timeSlotSize)
        {
            var message = new UpdateServiceMessage()
            {
                Id = id,
                Name = name,
                TimeSlotSize = timeSlotSize,
            };

            await _sendEndpointProvider.Send(message);
        }
    }
}
