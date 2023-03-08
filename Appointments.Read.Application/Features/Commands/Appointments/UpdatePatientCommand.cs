using Appointments.Read.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.Appointments
{
    public class UpdatePatientCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;

        public UpdatePatientCommandHandler(IAppointmentsRepository appointmentsRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (appointmentsRepository, mapper);

        public async Task<int> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.UpdatePatientAsync(request.
                Id,
                request.FullName,
                request.PhoneNumber,
                request.DateOfBirth);
        }
    }
}
