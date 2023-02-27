using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Services.Service.SwaggerExamples
{
    public class CreateServiceRequestExample : IExamplesProvider<CreateServiceRequest>
    {
        public CreateServiceRequest GetExamples() =>
            new()
            {
                Title = "Custom title",
                Price = 90,
                SpecializationId = new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                CategoryId = new Guid("D4928947-6BC5-4030-899E-702B8F47B2F4"),
                IsActive = true,
            };
    }
}
