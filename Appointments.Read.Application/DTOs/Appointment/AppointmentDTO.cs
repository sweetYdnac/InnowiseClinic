namespace Appointments.Read.Application.DTOs.Appointment
{
    public class AppointmentDTO
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
        public bool IsApproved { get; set; }
    }
}
