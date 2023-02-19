using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Messages;

namespace Profiles.API.Consumers
{
    public class UpdateSpecializationConsumer : IConsumer<UpdateSpecializationMessage>
    {
        private readonly IDoctorsService _doctorService;

        public UpdateSpecializationConsumer(IDoctorsService doctorService) => _doctorService = doctorService;

        public async Task Consume(ConsumeContext<UpdateSpecializationMessage> context)
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
