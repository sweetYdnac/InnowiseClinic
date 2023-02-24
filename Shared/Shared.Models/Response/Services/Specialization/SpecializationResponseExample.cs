using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Services.Specialization
{
    public class SpecializationResponseExample : IExamplesProvider<SpecializationResponse>
    {
        public SpecializationResponse GetExamples() =>
            new()
            {
                Title = "Dentist",
                IsActive = true,
            };
    }
}
