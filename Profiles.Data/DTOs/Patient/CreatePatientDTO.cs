namespace Profiles.Data.DTOs.Patient
{
    public class CreatePatientDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? AccountId { get; set; }
        public bool IsLinkedToAccount { get; set; }
    }
}
