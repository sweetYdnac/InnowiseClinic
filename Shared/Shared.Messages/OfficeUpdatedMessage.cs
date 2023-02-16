namespace Shared.Messages
{
    public class OfficeUpdatedMessage
    {
        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
