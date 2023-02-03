namespace Profiles.Domain.Entities
{
    public class PatientEntity : User
    {
        public bool IsLinkedToAccount { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
