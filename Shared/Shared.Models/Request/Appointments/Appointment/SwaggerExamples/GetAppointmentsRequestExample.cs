using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.Appointment.SwaggerExamples
{
    public class GetAppointmentsRequestExample : IExamplesProvider<GetAppointmentsRequest>
    {
        public GetAppointmentsRequest GetExamples() =>
            new()
            {
                CurrentPage = 2,
                PageSize = 3,
                Date = new DateOnly(2023, 05,24),
                DoctorFullName = "Ale",
                ServiceId = Guid.NewGuid(),
                OfficeId = Guid.NewGuid(),
                IsApproved = true,
            };
    }
}
