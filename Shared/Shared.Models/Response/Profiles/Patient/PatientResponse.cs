namespace Shared.Models.Response.Profiles.Patient
{
    public class PatientResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid PhotoId { get; set; }
    }
}
