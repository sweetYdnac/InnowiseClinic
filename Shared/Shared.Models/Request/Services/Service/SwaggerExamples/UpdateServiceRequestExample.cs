using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Services.Service.SwaggerExamples
{
    public class UpdateServiceRequestExample : IExamplesProvider<UpdateServiceRequest>
    {
        public UpdateServiceRequest GetExamples() =>
            new()
            {
                Title = "new title",
                Price = 30,
                SpecializationId = new Guid("6FF44FBF-8DE7-4322-AC02-68190750FBAD"),
                CategoryId = new Guid("6F1F47A7-120D-4DCF-AA33-A98FDA88BF25"),
                IsActive = false,
            };
    }
}
