using Shared.Core.Enums;

namespace Shared.Models.Request.Profiles.Doctor
{
    public class CreateDoctorRequest
    {
        public Guid Id { get; set; }
        public Guid? PhotoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
        public string SpecializationName { get; set; }
        public string OfficeAddress { get; set; }
        public AccountStatuses Status { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
