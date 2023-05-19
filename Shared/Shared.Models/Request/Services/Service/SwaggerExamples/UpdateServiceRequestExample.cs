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
                CategoryId = new Guid("6F1F47A7-120D-4DCF-AA33-A98FDA88BF25"),
                TimeSlotSize = 20,
                IsActive = false,
            };
    }
}
