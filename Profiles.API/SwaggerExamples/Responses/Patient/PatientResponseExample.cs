using Shared.Models.Response.Profiles.Patient;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Patient
{
    public class PatientResponseExample : IExamplesProvider<PatientResponse>
    {
        public PatientResponse GetExamples() =>
            new PatientResponse
            {
                FirstName = "Evgeny",
                LastName = "Koreba",
                MiddleName = "Sergeevich",
                DateOfBirth = new DateTime(1998, 07, 16)
            };
    }
}
