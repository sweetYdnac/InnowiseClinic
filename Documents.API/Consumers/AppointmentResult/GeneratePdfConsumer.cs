using AutoMapper;
using Documents.Business.DTOs;
using Documents.Business.Interfaces;
using MassTransit;
using Shared.Messages;

namespace Documents.API.Consumers.AppointmentResult
{
    public class GeneratePdfConsumer : IConsumer<GeneratePdfMessage>
    {
        private readonly IAppointmentResultsService _appointmentResultsService;
        private readonly IFileGeneratorService _fileGeneratorService;
        private readonly IMapper _mapper;

        public GeneratePdfConsumer(
            IAppointmentResultsService appointmentResultsService,
            IFileGeneratorService fileGeneratorService,
            IMapper mapper) =>
        (_appointmentResultsService, _fileGeneratorService, _mapper) =
        (appointmentResultsService, fileGeneratorService, mapper);

        public async Task Consume(ConsumeContext<GeneratePdfMessage> context)
        {
            var pdf = await _fileGeneratorService.GetPdfAppointmentResult(
                _mapper.Map<PdfResultDTO>(context.Message));

            await _appointmentResultsService.CreateAsync(context.Message.Id, pdf);
        }
    }
}
