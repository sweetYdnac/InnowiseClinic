using Shared.Models.Request.Offices;
using Swashbuckle.AspNetCore.Filters;

namespace Offices.API.SwaggerExamples.Requests
{
    public class CreateOfficeRequestExample : IExamplesProvider<CreateOfficeRequestModel>
    {
        public CreateOfficeRequestModel GetExamples() =>
            new CreateOfficeRequestModel
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
