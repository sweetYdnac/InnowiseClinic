using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.AppointmentResult.SwaggerExamples
{
    public class CreateAppointmentResultRequestExample : IExamplesProvider<CreateAppointmentResultRequest>
    {
        public CreateAppointmentResultRequest GetExamples() =>
            new()
            {
                Complaints = "Bla bla bla",
                Conclusion = "Healthy",
                Recomendations = "Go for a wolk",
                AppointmentId = Guid.NewGuid(),
            };
    }
}
