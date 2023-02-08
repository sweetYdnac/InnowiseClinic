using Shared.Models.Request.Profiles.Patient;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Requests.Patient
{
    public class GetMatchedPatientRequestExample : IExamplesProvider<GetMatchedPatientRequestModel>
    {
        public GetMatchedPatientRequestModel GetExamples() =>
            new GetMatchedPatientRequestModel
            {
                FirstName = "Jeff",
                LastName = "Cobra",
                MiddleName = "something",
                DateOfBirth = new DateTime(1999, 01, 22)
            };
    }
}
