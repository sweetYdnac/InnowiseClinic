using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.Appointment.SwaggerExamples
{
    public class GetTimeSlotsRequestExample : IExamplesProvider<GetTimeSlotsRequest>
    {
        public GetTimeSlotsRequest GetExamples() =>
            new()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                ServiceId = Guid.NewGuid(),
                DoctorId = Guid.NewGuid(),
                Duration = 20,
                StartTime = new TimeOnly(08,00),
                EndTime = new TimeOnly(20, 00),
            };
    }
}
