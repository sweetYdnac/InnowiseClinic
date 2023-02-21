using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Profiles.Patient.SwaggerExamples
{
    public class GetPatientsResponseExample : IExamplesProvider<PagedResponse<PatientInformationResponse>>
    {
        public PagedResponse<PatientInformationResponse> GetExamples() =>
            new(
                new PatientInformationResponse[]
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        FullName = "David Guetta",
                        PhoneNumber = "1234567890",
                    },
                    new()
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
