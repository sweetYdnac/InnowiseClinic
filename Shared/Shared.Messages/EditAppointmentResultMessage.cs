namespace Shared.Messages
{
    public class EditAppointmentResultMessage
    {
        public Guid Id { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recommendations { get; set; }
    }
}
