using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Messages;

namespace Profiles.API.Consumers
{
    public class DisableSpecializationConsumer : IConsumer<DisableSpecializationMessage>
    {
        private readonly IDoctorsService _doctorService;

        public DisableSpecializationConsumer(IDoctorsService doctorService) => _doctorService = doctorService;

        public async Task Consume(ConsumeContext<DisableSpecializationMessage> context)
        {
            await _doctorService.SetInactiveStatusAsync(context.Message.SpecializationId);
        }
    }
}
