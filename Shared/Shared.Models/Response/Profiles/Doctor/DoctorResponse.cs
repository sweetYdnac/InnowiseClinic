using Shared.Core.Enums;

namespace Shared.Models.Response.Profiles.Doctor
{
    public class DoctorResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string SpecializationName { get; set; }
        public string OfficeAddress { get; set; }
        public int CareerStartYear { get; set; }
        public Guid? PhotoId { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
