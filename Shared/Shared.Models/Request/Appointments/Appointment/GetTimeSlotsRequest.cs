namespace Shared.Models.Request.Appointments.Appointment
{
    public class GetTimeSlotsRequest
    {
        public Guid ServiceId { get; set; }
        public Guid? DoctorId { get; set; }
        public int Duration { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
