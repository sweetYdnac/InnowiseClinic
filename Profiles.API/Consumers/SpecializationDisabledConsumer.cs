using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Models.Messages;

namespace Profiles.API.Consumers
{
    public class SpecializationDisabledConsumer : IConsumer<SpecializationDisabled>
    {
        private readonly IDoctorsService _doctorService;

        public SpecializationDisabledConsumer(IDoctorsService doctorService) => _doctorService = doctorService;

        public async Task Consume(ConsumeContext<SpecializationDisabled> context)
        {
            await _doctorService.SetInactiveStatusAsync(context.Message.SpecializationId);
        }
    }
}
