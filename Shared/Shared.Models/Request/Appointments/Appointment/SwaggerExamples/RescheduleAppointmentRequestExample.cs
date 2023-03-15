using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.Appointment.SwaggerExamples
{
    public class RescheduleAppointmentRequestExample : IExamplesProvider<RescheduleAppointmentRequest>
    {
        public RescheduleAppointmentRequest GetExamples() =>
            new()
            {
                DoctorId = Guid.NewGuid(),
                OfficeId = Guid.NewGuid(),
                Date = new DateOnly(2023,3,10),
                Time = new TimeOnly(15,20),
                DoctorFullName = "Petr Solevoy ",
            };
    }
}
