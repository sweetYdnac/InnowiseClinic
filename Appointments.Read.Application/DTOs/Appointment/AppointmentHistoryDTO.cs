namespace Appointments.Read.Application.DTOs.Appointment
{
    public class AppointmentHistoryDTO
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
        public Guid? ResultId { get; set; }
        public bool IsApproved { get; set; }
    }
}
