namespace Shared.Models.Response.Appointments.AppointmentResult
{
    public class AppointmentResultResponse
    {
        public Guid Id { get; set; }
        public string PatientFullName { get; set; }
        public DateOnly PatientDateOfBirth { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; }
        public string DoctorSpecializationName { get; set; }
        public string ServiceName { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
    }
}
