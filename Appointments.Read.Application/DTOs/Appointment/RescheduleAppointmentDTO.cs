namespace Appointments.Read.Application.DTOs.Appointment
{
    public class RescheduleAppointmentDTO
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public string DoctorFullName { get; set; }
    }
}
