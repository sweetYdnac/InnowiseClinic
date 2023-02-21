using Shared.Core.Enums;

namespace Profiles.Data.DTOs.ReceptionistSummary
{
    public class UpdateReceptionistSummaryDTO
    {
        public string OfficeAddress { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
