namespace Shared.Models.Request.Appointments.Appointment
{
    public class RescheduleAppointmentRequest
    {
        public Guid DoctorId { get; set; }
        public Guid OfficeId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public string DoctorFullName { get; set; }
    }
}
