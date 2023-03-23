namespace Shared.Models.Request.Appointments.AppointmentResult
{
    public class CreateAppointmentResultRequest
    {
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recommendations { get; set; }
        public Guid AppointmentId { get; set; }

        public string PatientFullName { get; set; }
        public DateOnly PatientDateOfBirth { get; set; }
        public string DoctorFullName { get; set; }
        public string DoctorSpecializationName { get; set; }
        public string ServiceName { get; set; }
    }
}
