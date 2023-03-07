using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;

namespace Appointments.Read.Application.Features.Commands
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

        public UpdateServiceCommandHandler(IAppointmentsRepository appointmentsRepository) =>
            _appointmentsRepository = appointmentsRepository;

        public async Task<int> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            return await _appointmentsRepository.UpdateServiceAsync(
                request.Id,
                request.Name,
                request.TimeSlotSize);
        }
    }
}
