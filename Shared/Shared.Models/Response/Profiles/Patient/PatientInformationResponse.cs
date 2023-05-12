namespace Shared.Models.Response.Profiles.Patient
{
    public class PatientInformationResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
