using Shared.Core.Enums;

namespace Shared.Models.Response.Profiles.Doctor
{
    public class DoctorInformationResponse
    {
        public string FullName { get; set; }
        public string SpecializationName { get; set; }
        public string OfficeAddress { get; set; }
        public int Experience { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
