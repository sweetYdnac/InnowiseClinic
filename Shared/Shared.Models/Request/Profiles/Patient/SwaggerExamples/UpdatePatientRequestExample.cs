using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Profiles.Patient.SwaggerExamples
{
    public class UpdatePatientRequestExample : IExamplesProvider<UpdatePatientRequest>
    {
        public UpdatePatientRequest GetExamples() =>
            new()
            {
                FirstName = "Jack",
                LastName = "Sparrow",
                MiddleName = "None",
                DateOfBirth = new DateOnly(1976, 04, 14),
                PhoneNumber = "1234567890",
            };
    }
}
