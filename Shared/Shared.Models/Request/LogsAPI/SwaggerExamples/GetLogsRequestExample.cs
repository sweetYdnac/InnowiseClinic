using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Shared.Models.Request.LogsAPI.SwaggerExamples
{
    public class GetLogsRequestExample : IExamplesProvider<GetLogsRequest>
    {
        public GetLogsRequest GetExamples() =>
            new()
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                ApiName = "Offices.API",
                Code = HttpStatusCode.NotFound,
            };
    }
}
