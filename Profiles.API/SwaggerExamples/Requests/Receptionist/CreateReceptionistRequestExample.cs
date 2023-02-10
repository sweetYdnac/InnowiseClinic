using Shared.Models.Request.Profiles.Receptionist;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Requests.Receptionist
{
    public class CreateReceptionistRequestExample : IExamplesProvider<CreateReceptionistRequestModel>
    {
        public CreateReceptionistRequestModel GetExamples() =>
            new CreateReceptionistRequestModel
            {
                FirstName = "Jenna",
                LastName = "Ortega",
                MiddleName = "Some middle name",
                AccountId = Guid.NewGuid(),
                OfficeId = Guid.NewGuid(),
                OfficeAddress = "New York somestreet 10 6"
            };
    }
}
