namespace Shared.Models.Request.Profiles.Receptionist
{
    public class CreateReceptionistRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid AccountId { get; set; }
        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; }
    }
}
