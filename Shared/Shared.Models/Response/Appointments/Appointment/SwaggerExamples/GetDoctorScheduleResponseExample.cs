using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Appointments.Appointment.SwaggerExamples
{
    public class GetDoctorScheduleResponseExample :
        IMultipleExamplesProvider<PagedResponse<DoctorScheduledAppointmentResponse>>
    {
        public IEnumerable<SwaggerExample<PagedResponse<DoctorScheduledAppointmentResponse>>> GetExamples() =>
            new List<SwaggerExample<PagedResponse<DoctorScheduledAppointmentResponse>>>
            {
                new()
                {
                    Name = "Variant 1",
                    Value = new(
                    new DoctorScheduledAppointmentResponse[]
                    {
                        new()
                        {
                            StartTime = new TimeOnly(15,00),
                            EndTime = new TimeOnly(15,30),
                            PatientId = Guid.NewGuid(),
                            PatientFullName = "Ravshan Winner D",
                            ServiceName = "Healling",
                            IsApproved = true,
                            ResultId = Guid.NewGuid(),
                        }
                    },
                    2,
                    2,
                    6),
                },
                new()
                {
                    Name = "Variant 2",
                    Value = new(
                    new DoctorScheduledAppointmentResponse[]
                    {
                        new()
                        {
                            StartTime = new TimeOnly(10,20),
                            EndTime = new TimeOnly(10,30),
                            PatientId = Guid.NewGuid(),
                            PatientFullName = "Ravshan Winner D",
                            ServiceName = "Healling",
                            IsApproved = false,
                            ResultId = null,
                        }
                    },
                    1,
                    3,
                    4),
                }
            };
    }
}
