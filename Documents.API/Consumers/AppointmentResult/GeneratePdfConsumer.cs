using Documents.Business.Interfaces;
using MassTransit;
using Shared.Messages;

namespace Documents.API.Consumers.AppointmentResult
{
    public class GeneratePdfConsumer : IConsumer<GeneratePdfMessage>
    {
        private readonly IAppointmentResultsService _appointmentResultsService;

        public GeneratePdfConsumer(IAppointmentResultsService appointmentResultsService) =>
            _appointmentResultsService = appointmentResultsService;

        public async Task Consume(ConsumeContext<GeneratePdfMessage> context)
        {
            await _appointmentResultsService.UpdateOrCreateAsync(context.Message.Id, context.Message.Bytes);
        }
    }
}
