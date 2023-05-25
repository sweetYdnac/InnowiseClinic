namespace Shared.Models.Response.Appointments.Appointment
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid PatientId { get; set; }
        public string PatientFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
        public bool IsApproved { get; set; }
    }
}
