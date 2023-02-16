using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Messages;

namespace Profiles.API.Consumers
{
    public class SpecializationUpdatedConsumer : IConsumer<SpecializationUpdatedMessage>
    {
        private readonly IDoctorsService _doctorService;

        public SpecializationUpdatedConsumer(IDoctorsService doctorService) => _doctorService = doctorService;

        public async Task Consume(ConsumeContext<SpecializationUpdatedMessage> context)
        {
            var message = context.Message;

            await _doctorService.UpdateSpecializationName(message.SpecializationId, message.Name);

            if (!message.IsActive)
            {
                await _doctorService.SetInactiveStatusAsync(message.SpecializationId);
            }
        }
    }
}
