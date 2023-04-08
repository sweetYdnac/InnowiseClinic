using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Profiles.Patient.SwaggerExamples
{
    public class PatientResponseExample : IExamplesProvider<PatientResponse>
    {
        public PatientResponse GetExamples() =>
            new()
            {
                FirstName = "Evgeny",
                LastName = "Koreba",
                MiddleName = "Sergeevich",
                DateOfBirth = new DateOnly(1998, 07, 16),
                PhotoId = Guid.NewGuid(),
                PhoneNumber = "+375296666666",
            };
    }
}
