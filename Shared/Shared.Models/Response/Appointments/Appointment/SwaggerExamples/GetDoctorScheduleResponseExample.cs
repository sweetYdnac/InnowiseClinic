using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Appointments.Appointment.SwaggerExamples
{
    public class GetDoctorScheduleResponseExample :
        IExamplesProvider<PagedResponse<DoctorScheduledAppointmentResponse>>
    {
        public PagedResponse<DoctorScheduledAppointmentResponse> GetExamples() => new(
            new DoctorScheduledAppointmentResponse[]
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    StartTime = new TimeOnly(15,00),
                    EndTime = new TimeOnly(15,30),
                    PatientId = Guid.NewGuid(),
                    PatientFullName = "Ravshan Winner D",
                    ServiceName = "Healling",
                    IsApproved = true,
                    ResultId = Guid.NewGuid(),
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    StartTime = new TimeOnly(10,20),
                    EndTime = new TimeOnly(10,30),
                    PatientId = Guid.NewGuid(),
                    PatientFullName = "Ravshan Winner D",
                    ServiceName = "Healling",
                    IsApproved = false,
                    ResultId = null,
                }
            },
            2,
            2,
            6);
    }
}
