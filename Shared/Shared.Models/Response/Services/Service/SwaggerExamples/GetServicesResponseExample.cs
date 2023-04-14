using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Services.Service.SwaggerExamples
{
    public class GetServicesResponseExample : IExamplesProvider<PagedResponse<ServiceInformationResponse>>
    {
        public PagedResponse<ServiceInformationResponse> GetExamples() =>
            new(
                 new[]
                {
                    new ServiceInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        Title = "Therapist",
                        Price = 50,
                        CategoryTitle = "Diagnostics",
                        Duration = 30,
                        SpecializationId = Guid.NewGuid(),
                        IsActive = true,
                    },
                    new ServiceInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        Title = "Oculist",
                        Price = 30,
                        CategoryTitle = "Analyses",
                        Duration = 10,
                        SpecializationId = Guid.NewGuid(),
                        IsActive = false,
                    },

                },
                2,
                3,
                5
                );
    }
}
