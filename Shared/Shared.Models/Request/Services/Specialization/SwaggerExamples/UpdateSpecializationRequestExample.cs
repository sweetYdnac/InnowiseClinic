using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Services.Specialization.SwaggerExamples
{
    public class UpdateSpecializationRequestExample : IExamplesProvider<UpdateSpecializationRequest>
    {
        public UpdateSpecializationRequest GetExamples() =>
            new()
            {
                Title = "Therapist",
                IsActive = true,
            };
    }
}
