namespace Shared.Models.Messages
{
    public class OfficeUpdated
    {
        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
