using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Services.Service.SwaggerExamples
{
    public class ServiceResponseExample : IExamplesProvider<ServiceResponse>
    {
        public ServiceResponse GetExamples() =>
            new()
            {
                Title = "Custom service name",
                Price = 50,
                CategoryTitle = "Diagnostics",
                IsActive = true,
            };
    }
}
