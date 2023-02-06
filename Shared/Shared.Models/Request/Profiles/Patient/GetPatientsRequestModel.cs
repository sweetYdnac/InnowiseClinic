using Shared.Models.Parameters;

namespace Shared.Models.Request.Profiles.Patient
{
    public class GetPatientsRequestModel : PagingParameters
    {
        public string FullName { get; set; }
    }
}
