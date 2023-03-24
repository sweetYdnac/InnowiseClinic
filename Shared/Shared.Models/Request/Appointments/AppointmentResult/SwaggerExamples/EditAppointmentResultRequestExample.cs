using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.AppointmentResult.SwaggerExamples
{
    public class EditAppointmentResultRequestExample : IExamplesProvider<EditAppointmentResultRequest>
    {
        public EditAppointmentResultRequest GetExamples() =>
            new()
            {
                Complaints = "Bla bla bla",
                Conclusion = "Healthy",
                Recommendations = "Go for a wolk",

                PatientFullName = "Evgeny Koreba ",
                PatientDateOfBirth = new DateOnly(2000, 2, 13),
                DoctorFullName = "Doctor Octavius ",
                DoctorSpecializationName = "Custom specialization",
                ServiceName = "Filling",
                Date = DateTime.Now,
            };
    }
}
