using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Appointments.Appointment.SwaggerExamples
{

    public class RescheduleAppointmentResponseExample : IExamplesProvider<RescheduleAppointmentResponse>
    {
        public RescheduleAppointmentResponse GetExamples() =>
            new()
            {
                PatientId = Guid.NewGuid(),
                PatientFullName = "Evgeny Sweety T",
                PatientPhoneNumber = "123123",
                PatientDateOfBirth = new DateOnly(2000, 01, 15),

                DoctorId = Guid.NewGuid(),
                DoctorFullName = "Alex First M",
                DoctorSpecializationName = "Dentist",

                ServiceId = Guid.NewGuid(),
                ServiceName = "Some service",

                OfficeId = Guid.NewGuid(),
                OfficeAddress = "Homel, Belickogo 9 1",

                Date = new DateOnly(2023, 05, 30),
                Time = new TimeOnly(15,00),
                Duration = 30,
            };
    }
}
