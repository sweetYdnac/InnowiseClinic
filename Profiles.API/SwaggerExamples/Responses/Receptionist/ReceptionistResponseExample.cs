using Shared.Models.Response.Profiles.Receptionist;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Receptionist
{
    public class ReceptionistResponseExample : IExamplesProvider<ReceptionistResponse>
    {
        public ReceptionistResponse GetExamples() =>
            new ReceptionistResponse
            {
                FirstName = "Jack",
                LastName = "Sparrow",
                MiddleName = "Some middlename",
                OfficeAddress = "Boston str. 10, 11"
            };
    }
}
