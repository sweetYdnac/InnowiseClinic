using Shared.Models.Request.Profiles.Patient;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Requests.Patient
{
    public class UpdatePatientRequestExample : IExamplesProvider<UpdatePatientRequestModel>
    {
        public UpdatePatientRequestModel GetExamples() =>
            new UpdatePatientRequestModel
            {
                FirstName = "Jack",
                LastName = "Sparrow",
                MiddleName = "None",
                DateOfBirth = new DateTime(1976, 04, 14);
            };
    }
}
