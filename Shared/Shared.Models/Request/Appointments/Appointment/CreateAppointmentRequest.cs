namespace Shared.Models.Request.Appointments.Appointment
{
    public class CreateAppointmentRequest
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid OfficeId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int Duration { get; set; }

        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public DateOnly PatientDateOfBirth { get; set; }
        public string DoctorFullName { get; set; }
        public string DoctorSpecializationName { get; set; }
        public string ServiceName { get; set; }
        public string OfficeAddress { get; set; }
    }
}
