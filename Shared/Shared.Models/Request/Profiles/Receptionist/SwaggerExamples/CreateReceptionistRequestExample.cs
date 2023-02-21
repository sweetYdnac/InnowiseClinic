using Shared.Core.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Profiles.Receptionist.SwaggerExamples
{
    public class CreateReceptionistRequestExample : IExamplesProvider<CreateReceptionistRequest>
    {
        public CreateReceptionistRequest GetExamples() =>
            new()
            {
                PhotoId = Guid.NewGuid(),
                FirstName = "Jenna",
                LastName = "Ortega",
                MiddleName = "Some middle name",
                AccountId = Guid.NewGuid(),
                OfficeId = Guid.NewGuid(),
                OfficeAddress = "New York somestreet 10 6",
                Status = AccountStatuses.SickDay,
            };
    }
}
