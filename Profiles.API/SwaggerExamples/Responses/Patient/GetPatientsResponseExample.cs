using Shared.Models.Response.Profiles.Patient;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Patient
{
    public class GetPatientsResponseExample : IExamplesProvider<GetPatientsResponseModel>
    {
        public GetPatientsResponseModel GetExamples() =>
            new(
                new[]
                {
                    new PatientInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        FullName = "David Guetta",
                        PhoneNumber = "1234567890",
                    },
                    new PatientInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Martin Garrix NVM",
                        PhoneNumber = "123123123"
                    }
                },
                3,
                2,
                50);
    }
}
