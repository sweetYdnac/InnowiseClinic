using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.Appointments
{
    public class UpdateServiceCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TimeSlotSize { get; set; }
    }

    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;

        public UpdateServiceCommandHandler(IAppointmentsRepository appointmentsRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (appointmentsRepository, mapper);

        public async Task<int> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.UpdateServiceAsync(_mapper.Map<UpdateServiceDTO>(request));
        }
    }
}
