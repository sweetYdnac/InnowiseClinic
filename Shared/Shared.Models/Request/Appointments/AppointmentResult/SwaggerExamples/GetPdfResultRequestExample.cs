using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.AppointmentResult.SwaggerExamples
{
    public class GetPdfResultRequestExample : IExamplesProvider<GetPdfResultRequest>
    {
        public GetPdfResultRequest GetExamples() =>
            new()
            {
                Date = new DateTime(2023,04,22),
                PatientFullName = "Evgeny Koreba ",
                PatientDateOfBirth = new DateOnly(1998, 07, 16),
                DoctorFullName = "John Dalhback ",
                DoctorSpecializationName = "Therapist",
                ServiceName = "Healing",
                Complaints = "God bless you",
                Conclusion = "F",
                Recommendations = "Go pray",
            };
    }
}
