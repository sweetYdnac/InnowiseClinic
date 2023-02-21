using Shared.Core.Enums;

namespace Profiles.Data.DTOs.DoctorSummary
{
    public class CreateDoctorSummaryDTO
    {
        public Guid Id { get; set; }
        public string SpecializationName { get; set; }
        public string OfficeAddress { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
