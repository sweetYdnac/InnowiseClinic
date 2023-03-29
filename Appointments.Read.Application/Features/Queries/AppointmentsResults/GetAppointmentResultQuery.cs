using Appointments.Read.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Exceptions;
using Shared.Models.Response.Appointments.AppointmentResult;

namespace Appointments.Read.Application.Features.Queries.AppointmentsResults
{
    public class GetAppointmentResultQuery : IRequest<AppointmentResultResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetAppointmentResultQueryHandler : IRequestHandler<GetAppointmentResultQuery, AppointmentResultResponse>
    {
        private readonly IAppointmentsResultsRepository _appointmentsResultsRepository;
        private readonly IMapper _mapper;

        public GetAppointmentResultQueryHandler(
            IAppointmentsResultsRepository appointmentsResultsRepository,
            IMapper mapper) =>
        (_appointmentsResultsRepository, _mapper) = (appointmentsResultsRepository, mapper);

        public async Task<AppointmentResultResponse> Handle(GetAppointmentResultQuery request, CancellationToken cancellationToken)
        {
            var dto = await _appointmentsResultsRepository.GetByIdAsync(request.Id);

            return dto is null
                ? throw new NotFoundException($"Appointment result with id = {request.Id} doesn't exist")
                : _mapper.Map<AppointmentResultResponse>(dto);
        }
    }
}
