using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.Appointment.SwaggerExamples
{
    public class GetDoctorScheduleRequestExample : IExamplesProvider<GetDoctorScheduleRequest>
    {
        public GetDoctorScheduleRequest GetExamples() =>
            new()
            {
                CurrentPage = 2,
                PageSize = 2,
                Date = DateOnly.FromDateTime(DateTime.Now),
            };
    }
}
