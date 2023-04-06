namespace Shared.Models.Request.Profiles.Patient
{
    public class CreatePatientRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? PhotoId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
