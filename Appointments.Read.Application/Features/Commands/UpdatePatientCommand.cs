﻿using Appointments.Read.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Appointments.Read.Application.Features.Commands
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
        private readonly IAppointmentsResultsRepository _appointmentsResultRepository;
        private readonly IMapper _mapper;

        public UpdatePatientCommandHandler(
            IAppointmentsRepository appointmentsRepository,
            IAppointmentsResultsRepository appointmentsResultRepository,
            IMapper mapper) =>
        (_appointmentsRepository, _appointmentsResultRepository, _mapper) =
        (appointmentsRepository, appointmentsResultRepository, mapper);

        public async Task<int> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointmentsRepository.UpdatePatientAsync(request.Id, request.FullName, request.PhoneNumber);
            await _appointmentsResultRepository.UpdatePatientAsync(request.Id, request.DateOfBirth);

            return result;
        }
    }
}
