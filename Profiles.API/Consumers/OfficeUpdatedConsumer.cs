using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Messages;

namespace Profiles.API.Consumers
{
    public class OfficeUpdatedConsumer : IConsumer<OfficeUpdatedMessage>
    {
        private readonly IProfilesService _profilesService;

        public OfficeUpdatedConsumer(IProfilesService profilesService) => _profilesService = profilesService;

        public async Task Consume(ConsumeContext<OfficeUpdatedMessage> context)
        {
            var message = context.Message;

            await _profilesService.UpdateOfficeAddressAsync(message.OfficeId, message.OfficeAddress);

            if (!message.IsActive)
            {
                await _profilesService.SetInactiveStatusToPersonalAsync(message.OfficeId);
            }
        }
    }
}
