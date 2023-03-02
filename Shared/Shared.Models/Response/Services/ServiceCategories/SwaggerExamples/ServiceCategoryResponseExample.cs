using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Services.ServiceCategories.SwaggerExamples
{
    public class ServiceCategoryResponseExample : IExamplesProvider<ServiceCategoryResponse>
    {
        public ServiceCategoryResponse GetExamples() =>
            new()
            {
                Id = new Guid("99540760-4527-4511-8851-5B882D921E0A"),
                Title = "Analyses",
                TimeSlotSize = 10,
            };
    }
}
