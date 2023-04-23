using Appointments.Read.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Exceptions;
using Shared.Models.Response.Appointments.Appointment;

namespace Appointments.Read.Application.Features.Queries.Appointments
{
    public class GetAppointmentByIdQuery : IRequest<RescheduleAppointmentResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, RescheduleAppointmentResponse>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;

        public GetAppointmentByIdQueryHandler(IAppointmentsRepository appointmentsRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (appointmentsRepository, mapper);

        public async Task<RescheduleAppointmentResponse> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _appointmentsRepository.GetByIdAsync(request.Id);

            return entity is null ?
                throw new NotFoundException($"Appointment with id = {request.Id} doesn't exist") :
                _mapper.Map<RescheduleAppointmentResponse>(entity);
        }
    }
}
