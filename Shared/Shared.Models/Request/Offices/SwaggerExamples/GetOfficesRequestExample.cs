using Shared.Models.Request.Offices;
using Swashbuckle.AspNetCore.Filters;

namespace Offices.API.SwaggerExamples.Requests
{
    public class GetOfficesRequestExample : IExamplesProvider<GetOfficesRequest>
    {
        public GetOfficesRequest GetExamples() =>
            new()
            {
                CurrentPage = 1,
                PageSize = 10,
                IsActive = true,
            };
    }
}
