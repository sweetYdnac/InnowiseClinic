namespace Appointments.Read.Application.DTOs.Appointment
{
    public class DoctorScheduledAppointmentDTO
    {
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
