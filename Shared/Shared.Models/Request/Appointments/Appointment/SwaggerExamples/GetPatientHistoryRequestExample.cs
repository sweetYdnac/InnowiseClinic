using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.Appointment.SwaggerExamples
{
    public class GetPatientHistoryRequestExample : IExamplesProvider<GetPatientHistoryRequest>
    {
        public GetPatientHistoryRequest GetExamples() =>
            new()
            {
                CurrentPage = 2,
                PageSize = 10,
                IsFinished = true,
            };
    }
}
