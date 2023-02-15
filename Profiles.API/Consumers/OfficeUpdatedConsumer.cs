using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Models.Messages;

namespace Profiles.API.Consumers
{
    public class OfficeUpdatedConsumer : IConsumer<OfficeUpdated>
    {
        private readonly IProfilesService _profilesService;

        public OfficeUpdatedConsumer(IProfilesService profilesService) => _profilesService = profilesService;

        public async Task Consume(ConsumeContext<OfficeUpdated> context)
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
