using Documents.Business.Interfaces;
using MassTransit;
using Shared.Messages;

namespace Documents.API.Consumers.AppointmentResult
{
    public class GeneratePdfConsumer : IConsumer<GeneratePdfMessage>
    {
        private const string _contentType = "application/pdf";

        private readonly IAppointmentResultsService _appointmentResultsService;

        public GeneratePdfConsumer(IAppointmentResultsService appointmentResultsService) =>
            _appointmentResultsService = appointmentResultsService;

        public async Task Consume(ConsumeContext<GeneratePdfMessage> context)
        {
            await _appointmentResultsService.AddOrUpdateBlobAsync(context.Message.Id, context.Message.Content, _contentType);
        }
    }
}
