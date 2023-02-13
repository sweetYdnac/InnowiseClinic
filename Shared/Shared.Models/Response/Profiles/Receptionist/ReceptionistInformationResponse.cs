using Shared.Core.Enums;

namespace Shared.Models.Response.Profiles.Receptionist
{
    public class ReceptionistInformationResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string OfficeAddress { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
