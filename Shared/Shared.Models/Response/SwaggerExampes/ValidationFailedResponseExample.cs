using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.SwaggerExampes
{
    public class ValidationFailedResponseExample : IExamplesProvider<ValidationFailedResponse>
    {
        public ValidationFailedResponse GetExamples() =>
            new()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = 400,
                TraceId = "00-48f5c9195e884d7f416b6a0de1366773-e1c2e7c612378c2d-00",
                Errors = new List<Dictionary<string, string[]>>
                {
                    new()
                    {
                        { 
                            "Title", 
                            new string[] { "'Title' must not be empty." } 
                        },
                        {
                            "PageSize",
                            new string[] { "'Page Size' must be between 1 and 50. You entered 0." }
                        },
                                                {
                            "CurrentPage",
                            new string[] { "'Current Page' must be greater than '0'." }
                        }
                    }
                }
            };
    }
}
