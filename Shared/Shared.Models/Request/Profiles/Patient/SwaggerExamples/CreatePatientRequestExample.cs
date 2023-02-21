using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Profiles.Patient.SwaggerExamples
{
    public class CreatePatientRequestExample : IExamplesProvider<CreatePatientRequest>
    {
        public CreatePatientRequest GetExamples() =>
            new()
            {
                FirstName = "Scarlet",
                LastName = "Johansson",
                MiddleName = "nvm",
                DateOfBirth = new DateTime(1985, 10, 19),
                AccountId = Guid.NewGuid(),
                PhotoId = Guid.NewGuid(),
                PhoneNumber = "123321123",
            };
    }
}
