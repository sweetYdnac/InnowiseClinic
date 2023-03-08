namespace Shared.Models.Response.Appointments.Appointment
{
    public class DoctorScheduledAppointmentResponse
    {
        public Guid Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid PatientId { get; set; }
        public string PatientFullName { get; set; }
        public DateOnly PatientDateOfBirth { get; set; }
        public string DoctorFullName { get; set; }
        public string DoctorSpecializationName { get; set; }
        public string ServiceName { get; set; }
        public bool IsApproved { get; set; }
        public Guid? ResultId { get; set; }
    }
}
