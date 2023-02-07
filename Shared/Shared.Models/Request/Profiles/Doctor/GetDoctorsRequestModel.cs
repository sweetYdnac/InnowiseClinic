using Shared.Models.Parameters;

namespace Shared.Models.Request.Profiles.Doctor
{
    public class GetDoctorsRequestModel : PagingParameters
    {
        public Guid? OfficeId { get; set; }
        public Guid? SpecializationId { get; set; }
        public string FullName { get; set; }
    }
}
