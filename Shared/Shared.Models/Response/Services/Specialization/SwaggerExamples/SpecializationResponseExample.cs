using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Services.Specialization.SwaggerExamples
{
    public class SpecializationResponseExample : IExamplesProvider<SpecializationResponse>
    {
        public SpecializationResponse GetExamples() =>
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Dentist",
                IsActive = true,
            };
    }
}
