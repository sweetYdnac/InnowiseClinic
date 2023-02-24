using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Services.Specialization.SwaggerExamples
{
    public class CreateSpecializationRequestExample : IExamplesProvider<CreateSpecializationRequest>
    {
        public CreateSpecializationRequest GetExamples() =>
            new()
            {
                Title = "Dentist",
                IsActive = true,
            };
    }
}
