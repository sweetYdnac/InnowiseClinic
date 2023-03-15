namespace Shared.Models.Response.Appointments.Appointment
{
    public class AppointmentHistoryResponse
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
        public Guid? ResultId { get; set; }
    }
}
