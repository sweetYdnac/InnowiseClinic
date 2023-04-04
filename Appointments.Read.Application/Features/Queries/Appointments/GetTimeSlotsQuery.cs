using Appointments.Read.Application.Interfaces.Repositories;
using MediatR;
using Shared.Models.Response.Appointments.Appointment;

namespace Appointments.Read.Application.Features.Queries.Appointments
{
    public class GetTimeSlotsQuery : IRequest<TimeSlotsResponse>
    {
        public DateOnly Date { get; set; }
        public IEnumerable<Guid> Doctors { get; set; }
        public int Duration { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }

    public class GetTimeSlotsQueryHandler : IRequestHandler<GetTimeSlotsQuery, TimeSlotsResponse>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public GetTimeSlotsQueryHandler(IAppointmentsRepository appointmentsRepository) =>
            _appointmentsRepository = appointmentsRepository;

        public async Task<TimeSlotsResponse> Handle(GetTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentsRepository.GetAppointmentsAsync(
                    request.Date, request.Doctors);

            var timeSlots = Enumerable.Range(
                0,
                ((int)(request.EndTime - request.StartTime).TotalMinutes / 10) - request.Duration / 10 + 1)
                .Select(i => request.StartTime.AddMinutes(i * 10))
                .ToDictionary(time => time, time => new HashSet<Guid>(request.Doctors));

            foreach (var appointment in appointments)
            {
                var start = appointment.StartTime.AddMinutes(10 - request.Duration);
                var end = appointment.EndTime;

                while (start <= end)
                {
                    timeSlots[start].Remove(appointment.DoctorId);
                    start = start.AddMinutes(10);
                }
            }

            timeSlots = timeSlots
                .Where(s => s.Value.Count > 0)
                .ToDictionary(s => s.Key, s => s.Value);

            return new TimeSlotsResponse { TimeSlots = timeSlots };
        }
    }
}
