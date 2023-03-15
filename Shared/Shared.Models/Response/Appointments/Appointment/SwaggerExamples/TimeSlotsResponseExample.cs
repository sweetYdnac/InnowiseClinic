using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Appointments.Appointment.SwaggerExamples
{
    public class TimeSlotsResponseExample : IExamplesProvider<TimeSlotsResponse>
    {
        public TimeSlotsResponse GetExamples() =>
            new()
            {
                TimeSlots = new Dictionary<TimeOnly, HashSet<Guid>>()
                {
                    {
                        new TimeOnly(08,00),
                        new HashSet<Guid>()
                        {
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                        }
                    },
                    {
                        new TimeOnly(08,30),
                        new HashSet<Guid>()
                        {
                            Guid.NewGuid(),
                        }
                    },
                                        {
                        new TimeOnly(10,20),
                        new HashSet<Guid>()
                        {
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                        }
                    }
                }
            };
    }
}
