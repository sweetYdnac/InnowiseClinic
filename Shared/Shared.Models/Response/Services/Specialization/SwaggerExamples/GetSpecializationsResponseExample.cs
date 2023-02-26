using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Services.Specialization.SwaggerExamples
{
    public class GetSpecializationsResponseExample : IExamplesProvider<PagedResponse<SpecializationResponse>>
    {
        public PagedResponse<SpecializationResponse> GetExamples() =>
            new(
                new[]
                {
                    new SpecializationResponse
                    {
                        Title = "Therapist",
                        IsActive = true,
                    },
                    new SpecializationResponse
                    {
                        Title = "Oculist",
                        IsActive = false,
                    }
                },
                2,
                3,
                5
                );
    }
}
