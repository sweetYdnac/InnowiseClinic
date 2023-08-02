using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.LogsAPI.SwaggerExamples
{
    public class GetLogResponseExample : IExamplesProvider<LogResponse>
    {
        public LogResponse GetExamples() =>
            new()
            {
                Id = ObjectId.GenerateNewId(),
                DateTime = DateTime.UtcNow,
                ApiName = "OfficeAPI",
                Route = $"/api/offices/{Guid.NewGuid()}",
                Code = System.Net.HttpStatusCode.NotFound,
                Message = "Some message",
                Details = "some stack trace",
            };
    }
}
