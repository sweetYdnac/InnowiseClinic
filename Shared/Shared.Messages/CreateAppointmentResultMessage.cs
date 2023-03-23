namespace Shared.Messages
{
    public class CreateAppointmentResultMessage
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recommendations { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
