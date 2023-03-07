namespace Shared.Models.Response.Appointments.Appointment
{
    public class DoctorScheduledAppointmentResponse
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string PatientFullName { get; set; }
        public string ServiceName { get; set; }
        public bool IsApproved { get; set; }
        public Guid? ResultId { get; set; }
    }
}
