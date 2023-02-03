namespace Shared.Models.Request.Profiles.Patient
{
    public class CreatePatientRequestModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MiddleName { get; set; }
        public Guid AccountId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
