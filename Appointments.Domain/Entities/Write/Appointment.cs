using Appointments.Domain.Common;

namespace Appointments.Domain.Entities.Write
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
