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
                PatientDateOfBirth = new DateOnly(2000, 2, 13),
                DoctorFullName = "Doctor Octavius ",
                DoctorSpecializationName = "Custom specialization",
                ServiceName = "Filling",
                OfficeAddress = "Homel, belickogo 9 1",
            };
    }
}
