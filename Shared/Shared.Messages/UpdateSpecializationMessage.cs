namespace Shared.Messages
{
    public class UpdateSpecializationMessage
    {
        public Guid SpecializationId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
