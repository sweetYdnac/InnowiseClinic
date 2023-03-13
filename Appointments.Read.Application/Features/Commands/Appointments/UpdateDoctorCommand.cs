using Appointments.Read.Application.DTOs.Appointment;
using Appointments.Read.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Appointments.Read.Application.Features.Commands.Appointments
{
    public class UpdateDoctorCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string SpecializationName { get; set; }
        public Guid OfficeId { get; set; }
    }

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, int>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IMapper _mapper;

        public UpdateDoctorCommandHandler(IAppointmentsRepository appointmentsRepository, IMapper mapper) =>
            (_appointmentsRepository, _mapper) = (appointmentsRepository, mapper);

        public async Task<int> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointmentsRepository.UpdateDoctorAsync(
                _mapper.Map<UpdateDoctorDTO>(request));

            return result;
        }
    }
}
