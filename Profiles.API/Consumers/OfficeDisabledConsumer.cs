using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Messages;

namespace Profiles.API.Consumers
{
    public class OfficeDisabledConsumer : IConsumer<OfficeDisabledMessage>
    {
        private readonly IProfilesService _profilesService;

        public OfficeDisabledConsumer(IProfilesService profilesService) => _profilesService = profilesService;

        public async Task Consume(ConsumeContext<OfficeDisabledMessage> context)
        {
            await _profilesService.SetInactiveStatusToPersonalAsync(context.Message.OfficeId);
        }
    }
}
