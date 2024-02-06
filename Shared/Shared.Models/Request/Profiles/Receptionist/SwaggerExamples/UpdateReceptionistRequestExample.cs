using Shared.Core.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Profiles.Receptionist.SwaggerExamples
{
    public class UpdateReceptionistRequestExample : IExamplesProvider<UpdateReceptionistRequest>
    {
        public UpdateReceptionistRequest GetExamples() =>
            new()
            {
                PhotoId = null,
                FirstName = "Jenna",
                LastName = "Ortega",
                MiddleName = "Some middle name",
                OfficeId = Guid.NewGuid(),
                OfficeAddress = "New York somestreet 10 6",
                Status = AccountStatuses.SickLeave,
            };
    }
}
