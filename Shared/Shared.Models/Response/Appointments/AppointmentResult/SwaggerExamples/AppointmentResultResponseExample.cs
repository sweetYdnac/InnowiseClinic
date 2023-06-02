using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Appointments.AppointmentResult.SwaggerExamples
{
    public class AppointmentResultResponseExample : IExamplesProvider<AppointmentResultResponse>
    {
        public AppointmentResultResponse GetExamples() =>
            new()
            {
                Date = DateTime.Now,
                PatientFullName = "Evgeny Koreba ",
                PatientDateOfBirth = new DateOnly(1998,07,16),
                DoctorId = Guid.NewGuid(),
                DoctorFullName = "John Dalhback ",
                DoctorSpecializationName = "Therapist",
                ServiceName = "Healing",
                Complaints = "God bless you",
                Conclusion = "F",
                Recommendations = "Go pray",
            };
    }
}
