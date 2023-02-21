using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Messages;

namespace Profiles.API.Consumers
{
    public class DisableOfficeConsumer : IConsumer<DisableOfficeMessage>
    {
        private readonly IProfilesService _profilesService;

        public DisableOfficeConsumer(IProfilesService profilesService) => _profilesService = profilesService;

        public async Task Consume(ConsumeContext<DisableOfficeMessage> context)
        {
            await _profilesService.SetInactiveStatusToPersonalAsync(context.Message.OfficeId);
        }
    }
}
