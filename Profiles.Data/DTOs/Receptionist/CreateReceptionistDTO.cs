using Shared.Core.Enums;

namespace Profiles.Data.DTOs.Receptionist
{
    public class CreateReceptionistDTO
    {
        public Guid Id { get; set; }
        public Guid? PhotoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; }
        public AccountStatuses Status { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
