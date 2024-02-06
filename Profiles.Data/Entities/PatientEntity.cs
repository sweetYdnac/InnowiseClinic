namespace Profiles.Data.Entities
{
    public class PatientEntity : User
    {
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
