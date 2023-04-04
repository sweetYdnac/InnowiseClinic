namespace Shared.Models.Request.Appointments.Appointment
{
    public class GetTimeSlotsRequest
    {
        public DateOnly Date { get; set; }
        public IEnumerable<Guid> Doctors { get; set; }
        public int Duration { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
