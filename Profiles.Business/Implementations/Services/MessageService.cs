using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Core.Enums;
using Shared.Messages;

namespace Profiles.Business.Implementations.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageService(ISendEndpointProvider sendEndpointProvider) => _sendEndpointProvider = sendEndpointProvider;

        public async Task SendAccountStatusUpdatedMessageAsync(Guid accountId, AccountStatuses status, string updaterId)
        {
            var message = new AccountStatusUpdatedMessage
            {
                AccountId = accountId,
                Status = status,
                UpdaterId = updaterId,
            };

            await _sendEndpointProvider.Send(message);
        }

        public async Task SendProfileDeletedMessageAsync(Guid photoId)
        {
            await _sendEndpointProvider.Send(new ProfileDeletedMessage { PhotoId = photoId });
        }
    }
}
