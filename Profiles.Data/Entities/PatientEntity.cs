namespace Profiles.Data.Entities
{
    public class PatientEntity : User
    {
        public bool IsLinkedToAccount { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
