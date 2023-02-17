using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Offices.SwaggerExamples
{
    public class CreateOfficeRequestExample : IExamplesProvider<CreateOfficeRequest>
    {
        public CreateOfficeRequest GetExamples() =>
            new()
            {
                PhotoId = Guid.NewGuid(),
                City = "Grodno",
                Street = "Pushkinskaya",
                HouseNumber = "10",
                OfficeNumber = "20",
                RegistryPhoneNumber = "88005553535",
                IsActive = true
            };
    }
}
