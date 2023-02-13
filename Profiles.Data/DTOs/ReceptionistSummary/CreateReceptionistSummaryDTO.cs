using Shared.Core.Enums;

namespace Profiles.Data.DTOs.ReceptionistSummary
{
    public class CreateReceptionistSummaryDTO
    {
        public Guid Id { get; set; }
        public string OfficeAddress { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
