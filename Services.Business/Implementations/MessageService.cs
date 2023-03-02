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
    }
}
