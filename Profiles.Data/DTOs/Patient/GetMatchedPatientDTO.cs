namespace Profiles.Data.DTOs.Patient
{
    public class GetMatchedPatientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
