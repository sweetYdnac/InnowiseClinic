using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Models.Messages;

namespace Profiles.API.Consumers
{
    public class OfficeDisabledConsumer : IConsumer<OfficeDisabled>
    {
        private readonly IProfilesService _profilesService;

        public OfficeDisabledConsumer(IProfilesService profilesService) => _profilesService = profilesService;

        public async Task Consume(ConsumeContext<OfficeDisabled> context)
        {
            await _profilesService.SetInactiveStatusToPersonalAsync(context.Message.OfficeId);
        }
    }
}
