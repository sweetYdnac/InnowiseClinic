using Appointments.Write.Domain.Common;

namespace Appointments.Write.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ServiceId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public bool IsApproved { get; set; }

        public AppointmentResult AppointmentResult { get; set; }
    }
}
