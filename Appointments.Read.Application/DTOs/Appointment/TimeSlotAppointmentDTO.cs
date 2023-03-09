namespace Appointments.Read.Application.DTOs.Appointment
{
    public class TimeSlotAppointmentDTO
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid DoctorId { get; set; }
    }
}
