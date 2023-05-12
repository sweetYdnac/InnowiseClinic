using Shared.Core.Enums;

namespace Shared.Models.Response.Profiles.Doctor
{
    public class DoctorInformationResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; }
        public int Experience { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public AccountStatuses Status { get; set; }
        public Guid? PhotoId { get; set; }
    }
}
