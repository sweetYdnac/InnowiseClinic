using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Services.ServiceCategories.SwaggerExamples
{
    public class GetCategoriesResponseExample : IExamplesProvider<GetCategoriesResponse>
    {
        public GetCategoriesResponse GetExamples() =>
            new()
            {
                Categories = new ServiceCategoryResponse[]
                {
                    new()
                    {
                        Id = new Guid("99540760-4527-4511-8851-5B882D921E0A"),
                        Title = "Analyses",
                        TimeSlotSize = 10,
                    },
                    new()
                    {
                        Id = new Guid("D4928947-6BC5-4030-899E-702B8F47B2F4"),
                        Title = "Diagnostics",
                        TimeSlotSize = 30,
                    },
                    new()
                    {
                        Id = new Guid("6F1F47A7-120D-4DCF-AA33-A98FDA88BF25"),
                        Title = "Consultation",
                        TimeSlotSize = 20,
                    },
                }
            };
    }
}
