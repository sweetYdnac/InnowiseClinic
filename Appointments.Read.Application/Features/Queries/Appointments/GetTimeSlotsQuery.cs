using Appointments.Read.Application.Interfaces.Repositories;
using Appointments.Read.Application.Interfaces.Services;
using MediatR;
using Shared.Core.Extensions;
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
        private readonly IDateTimeProvider _dateTimeProvider;

        public GetTimeSlotsQueryHandler(IAppointmentsRepository
            appointmentsRepository,
            IDateTimeProvider dateTimeProvider) =>
        (_appointmentsRepository, _dateTimeProvider) = (appointmentsRepository, dateTimeProvider);

        public async Task<TimeSlotsResponse> Handle(GetTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var startTime = _dateTimeProvider.Now().Day.Equals(request.Date.Day)
                ? TimeOnly.FromDateTime(_dateTimeProvider.Now().Ceiling(TimeSpan.FromMinutes(10)))
                : request.StartTime;

            if ((request.EndTime - startTime).TotalMinutes < request.Duration)
            {
                return new TimeSlotsResponse { TimeSlots = new Dictionary<TimeOnly, HashSet<Guid>>() };
            }

            var appointments = await _appointmentsRepository.GetAppointmentsAsync(
                    request.Date, request.Doctors);

            var timeSlots = Enumerable.Range(
                0,
                ((int)(request.EndTime - startTime).TotalMinutes / 10) - (request.Duration / 10) + 1)
                .Select(i => startTime.AddMinutes(i * 10))
                .ToDictionary(time => time, time => new HashSet<Guid>(request.Doctors));

            foreach (var appointment in appointments)
            {
                var start = appointment.StartTime.AddMinutes(10 - request.Duration);
                var end = appointment.EndTime;

                while (start <= end)
                {
                    if(timeSlots.TryGetValue(start, out var slot))
                    {
                        slot.Remove(appointment.DoctorId);
                    }

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
