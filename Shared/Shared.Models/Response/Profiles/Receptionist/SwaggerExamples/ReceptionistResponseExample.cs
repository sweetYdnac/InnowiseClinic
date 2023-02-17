using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Profiles.Receptionist.SwaggerExamples
{
    public class ReceptionistResponseExample : IExamplesProvider<ReceptionistResponse>
    {
        public ReceptionistResponse GetExamples() =>
            new()
            {
                PhotoId = Guid.NewGuid(),
                FirstName = "Jack",
                LastName = "Sparrow",
                MiddleName = "Some middlename",
                OfficeAddress = "Boston str. 10, 11"
            };
    }
}
