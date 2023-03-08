using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Appointments.Appointment.SwaggerExamples
{
    public class CreateAppointmentRequestExample : IExamplesProvider<CreateAppointmentRequest>
    {
        public CreateAppointmentRequest GetExamples() =>
            new()
            {
                PatientId = Guid.NewGuid(),
                DoctorId = Guid.NewGuid(),
                ServiceId = Guid.NewGuid(),
                OfficeId = Guid.NewGuid(),
                Date = new DateOnly(2023, 3, 20),
                Time = new TimeOnly(14, 00),
                Duration = 30,
                PatientFullName = "Evgeny Koreba ",
                PatientPhoneNumber = "88005553535",
                DoctorFullName = "Doctor Octavius ",
                ServiceName = "Filling"
            };
    }
}
