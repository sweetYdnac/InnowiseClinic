using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Appointments.Appointment.SwaggerExamples
{
    public class GetPatientHistoryResponseExample : IExamplesProvider<PagedResponse<AppointmentHistoryResponse>>
    {
        public PagedResponse<AppointmentHistoryResponse> GetExamples() =>
            new(
                new AppointmentHistoryResponse[]
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Date = new DateOnly(2023,04,15),
                        StartTime = new TimeOnly(13,40),
                        EndTime = new TimeOnly(14,10),
                        DoctorFullName = "Some name ",
                        ServiceName = "Therapy",
                        ResultId = Guid.NewGuid(),
                        IsApproved = true,
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Date = new DateOnly(2023,05,11),
                        StartTime = new TimeOnly(11,20),
                        EndTime = new TimeOnly(11,30),
                        DoctorFullName = "Some other name",
                        ServiceName = "Therapy",
                        ResultId = null,
                        IsApproved = false,
                    }
                },
                2,
                3,
                10);
    }
}
