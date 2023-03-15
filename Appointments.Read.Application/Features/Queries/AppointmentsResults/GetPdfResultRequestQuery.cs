using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Interfaces.Services;
using AutoMapper;
using MediatR;
using Shared.Models.Response.Appointments.AppointmentResult;

namespace Appointments.Read.Application.Features.Queries.AppointmentsResults
{
    public class GetPdfResultQuery : IRequest<PdfResultResponse>
    {
        public DateTime Date { get; set; }
        public string PatientFullName { get; set; }
        public DateOnly PatientDateOfBirth { get; set; }
        public string DoctorFullName { get; set; }
        public string DoctorSpecializationName { get; set; }
        public string ServiceName { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recommendations { get; set; }
    }

    public class GetPdfResultHandler : IRequestHandler<GetPdfResultQuery, PdfResultResponse>
    {
        private readonly IFileGeneratorService _fileGeneratorService;
        private readonly IMapper _mapper;

        public GetPdfResultHandler(IFileGeneratorService fileGeneratorService, IMapper mapper) =>
            (_fileGeneratorService, _mapper) = (fileGeneratorService, mapper);

        public async Task<PdfResultResponse> Handle(GetPdfResultQuery request, CancellationToken cancellationToken)
        {
            return await _fileGeneratorService.GetPdfAppointmentResult(
                _mapper.Map<PdfResultDTO>(request));
        }
    }
}
