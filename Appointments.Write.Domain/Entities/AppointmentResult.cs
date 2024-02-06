using Appointments.Write.Domain.Common;

namespace Appointments.Write.Domain.Entities
{
    public class AppointmentResult : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recommendations { get; set; }

        public Appointment Appointment { get; set; }
    }
}
