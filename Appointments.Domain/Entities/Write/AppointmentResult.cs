using Appointments.Domain.Common;

namespace Appointments.Domain.Entities.Write
{
    public class AppointmentResult : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }

        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
