using Shared.Models.Response.Profiles.Patient;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Patient
{
    public class GetPatientsResponseExample : IExamplesProvider<GetPatientsResponseModel>
    {
        public GetPatientsResponseModel GetExamples() =>
            new GetPatientsResponseModel(
                new[]
                {
                    new PatientInformationResponse
                    {
                        FullName = "David Guetta"
                    },
                    new PatientInformationResponse
                    {
                        FullName = "Martin Garrix NVM"
                    }
                },
                3,
                15,
                100);
    }
}
