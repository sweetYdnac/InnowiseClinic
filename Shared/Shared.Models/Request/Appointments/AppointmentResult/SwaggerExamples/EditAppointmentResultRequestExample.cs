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
                Recomendations = "Go for a wolk",
            };
    }
}
