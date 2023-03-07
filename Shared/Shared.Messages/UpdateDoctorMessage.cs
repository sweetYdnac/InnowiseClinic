namespace Shared.Messages
{
    public class UpdateDoctorMessage
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string SpecializationName { get; set; }
    }
}
