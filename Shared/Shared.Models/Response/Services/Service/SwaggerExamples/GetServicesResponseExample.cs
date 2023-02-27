using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Services.Service.SwaggerExamples
{
    public class GetServicesResponseExample : IExamplesProvider<PagedResponse<ServiceResponse>>
    {
        public PagedResponse<ServiceResponse> GetExamples() =>
            new(
                 new[]
                {
                    new ServiceResponse
                    {
                        Id = Guid.NewGuid(),
                        Title = "Therapist",
                        Price = 50,
                        CategoryTitle = "Diagnostics",
                        IsActive = true,
                    },
                    new ServiceResponse
                    {
                        Id = Guid.NewGuid(),
                        Title = "Oculist",
                        Price = 30,
                        CategoryTitle = "Analyses",
                        IsActive = false,
                    },

                },
                2,
                3,
                5
                );
    }
}
