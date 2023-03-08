using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Appointments.Appointment.SwaggerExamples
{
    public class GetAppointmentsResponseExample : IMultipleExamplesProvider<PagedResponse<AppointmentResponse>>
    {
        public IEnumerable<SwaggerExample<PagedResponse<AppointmentResponse>>> GetExamples() =>
            new List<SwaggerExample<PagedResponse<AppointmentResponse>>>
            {
                new()
                {
                    Name = "Variant 1",
                    Value = new(
                        new AppointmentResponse[]
                        {
                            new()
                            {
                                StartTime = new TimeOnly(12,30),
                                EndTime = new TimeOnly(12,40),
                                PatientFullName = "Ravshan Winner D",
                                PatientPhoneNumber = "88005553535",
                                DoctorFullName = "Slava Pumpkin ",
                                ServiceName = "Filling",
                                IsApproved = true,
                            }
                        },
                        2,
                        1,
                        5),
                },
                new()
                {
                    Name = "Variant 2",
                    Value = new(
                        new AppointmentResponse[]
                        {
                            new()
                            {
                                StartTime = new TimeOnly(09,30),
                                EndTime = new TimeOnly(10,00),
                                PatientFullName = "Ravshan Winner D",
                                PatientPhoneNumber = "88005553535",
                                DoctorFullName = "Slava Pumpkin ",
                                ServiceName = "Healing",
                                IsApproved = false,
                            }
                        },
                        1,
                        1,
                        4),
                }
            };
    }
}
