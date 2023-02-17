using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Profiles.Patient.SwaggerExamples
{
    public class GetPatientsResponseExample : IExamplesProvider<GetPatientsResponse>
    {
        public GetPatientsResponse GetExamples() =>
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
