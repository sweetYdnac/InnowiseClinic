using Shared.Models.Request.Profiles.Patient;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Requests.Patient
{
    public class GetPatientsRequestExample : IExamplesProvider<GetPatientsRequestModel>
    {
        public GetPatientsRequestModel GetExamples() =>
            new GetPatientsRequestModel
            {
                FullName = "evgen",
                PageNumber = 2,
                PageSize = 10
            };
    }
}
