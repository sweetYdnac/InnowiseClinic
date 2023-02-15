namespace Shared.Models.Messages
{
    public class SpecializationUpdated
    {
        public Guid SpecializationId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
