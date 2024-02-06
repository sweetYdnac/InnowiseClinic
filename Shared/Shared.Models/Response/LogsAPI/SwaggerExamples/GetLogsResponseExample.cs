using MongoDB.Bson;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.LogsAPI.SwaggerExamples
{
    public class GetLogsResponseExample : IExamplesProvider<PagedResponse<LogResponse>>
    {
        public PagedResponse<LogResponse> GetExamples() =>
            new(
                new LogResponse[]
                {
                    new()
                    {
                        Id = ObjectId.GenerateNewId(),
                        DateTime = DateTime.UtcNow,
                        ApiName = "OfficeAPI",
                        Route = $"/api/offices/{Guid.NewGuid()}",
                        Code = System.Net.HttpStatusCode.NotFound,
                        Message = "Some message",
                        Details = "some stack trace",
                    }
                },
                2,
                5,
                15
                );
    }
}
