namespace Shared.Messages
{
    public class UpdateOfficeMessage
    {
        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
