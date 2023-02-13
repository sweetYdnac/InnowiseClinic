using Shared.Core.Enums;

namespace Profiles.Data.DTOs.DoctorSummary
{
    public class UpdateDoctorSummaryDTO
    {
        public string SpecializationName { get; set; }
        public string OfficeAddress { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
