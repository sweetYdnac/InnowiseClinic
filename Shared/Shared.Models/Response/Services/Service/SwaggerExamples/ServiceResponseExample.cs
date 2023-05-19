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
                CategoryId = Guid.NewGuid(),
                CategoryTitle = "Diagnostics",
                IsActive = true,
            };
    }
}
