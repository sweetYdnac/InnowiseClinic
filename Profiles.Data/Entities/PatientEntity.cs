namespace Profiles.Data.Entities
{
    public class PatientEntity : User
    {
        public bool IsLinkedToAccount { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
