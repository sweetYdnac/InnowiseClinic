using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Messages;

namespace Profiles.API.Consumers
{
    public class UpdateOfficeConsumer : IConsumer<UpdateOfficeMessage>
    {
        private readonly IProfilesService _profilesService;

        public UpdateOfficeConsumer(IProfilesService profilesService) => _profilesService = profilesService;

        public async Task Consume(ConsumeContext<UpdateOfficeMessage> context)
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
