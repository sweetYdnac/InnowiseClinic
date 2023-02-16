using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Messages;

namespace Profiles.API.Consumers
{
    public class SpecializationDisabledConsumer : IConsumer<SpecializationDisabledMessage>
    {
        private readonly IDoctorsService _doctorService;

        public SpecializationDisabledConsumer(IDoctorsService doctorService) => _doctorService = doctorService;

        public async Task Consume(ConsumeContext<SpecializationDisabledMessage> context)
        {
            await _doctorService.SetInactiveStatusAsync(context.Message.SpecializationId);
        }
    }
}
