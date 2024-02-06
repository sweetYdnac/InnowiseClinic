namespace Shared.Models.Response.Profiles.Patient
{
    public class PatientResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Guid? PhotoId { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
