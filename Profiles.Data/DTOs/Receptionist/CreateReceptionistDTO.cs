namespace Profiles.Data.DTOs.Receptionist
{
    public class CreateReceptionistDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid AccountId { get; set; }
        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; }
    }
}
