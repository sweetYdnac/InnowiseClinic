using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.LogsAPI.SwaggerExamples
{
    public class UpdateLogRequestExample : IExamplesProvider<UpdateLogRequest>
    {
        public UpdateLogRequest GetExamples() =>
            new()
            {
                ApiName = "OfficeAPI",
                Route = $"/api/offices/{Guid.NewGuid()}",
                Code = System.Net.HttpStatusCode.NotFound,
                Message = "Some message",
                Details = "some stack trace",
            };
    }
}
