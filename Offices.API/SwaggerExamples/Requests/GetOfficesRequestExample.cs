using Shared.Models.Request.Offices;
using Swashbuckle.AspNetCore.Filters;

namespace Offices.API.SwaggerExamples.Requests
{
    public class GetOfficesRequestExample : IExamplesProvider<GetOfficesRequestModel>
    {
        public GetOfficesRequestModel GetExamples() =>
            new GetOfficesRequestModel
            {
                PageNumber = 1,
                PageSize = 10
            };
    }
}
