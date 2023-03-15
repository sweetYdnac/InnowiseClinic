namespace Shared.Models.Request.Profiles.Patient
{
    public class UpdatePatientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
