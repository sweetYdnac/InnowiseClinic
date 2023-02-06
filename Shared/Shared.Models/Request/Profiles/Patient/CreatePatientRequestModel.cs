namespace Shared.Models.Request.Profiles.Patient
{
    public class CreatePatientRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? AccountId { get; set; }
        public bool IsLinkedToAccount { get; set; }
    }
}
