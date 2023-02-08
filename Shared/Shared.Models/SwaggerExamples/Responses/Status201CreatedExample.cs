using Shared.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.SwaggerExamples.Responses
{
    public class Status201Example : IExamplesProvider<Status201Response>
    {
        public Status201Response GetExamples() => new Status201Response { Id = Guid.NewGuid() };
    }
}
