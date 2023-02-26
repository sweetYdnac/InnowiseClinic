using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.SwaggerExampes
{
    public class ValidationFailedResponseExample : IExamplesProvider<ValidationFailedResponse>
    {
        public ValidationFailedResponse GetExamples() =>
            new()
            {

            };
    }
}
