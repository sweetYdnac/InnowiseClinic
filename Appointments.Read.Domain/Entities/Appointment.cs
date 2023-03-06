using Appointments.Read.Domain.Common;

namespace Appointments.Read.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int Duration { get; set; }
        public bool IsApproved { get; set; }

        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }

        public AppointmentResult AppointmentResult { get; set; }
    }
}
