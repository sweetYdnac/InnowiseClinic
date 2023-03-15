namespace Shared.Messages
{
    public class UpdateServiceMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TimeSlotSize { get; set; }
    }
}
