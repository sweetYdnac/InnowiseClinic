using Shared.Models.Request.Offices;
using Swashbuckle.AspNetCore.Filters;

namespace Offices.API.SwaggerExamples.Requests
{
    public class UpdateOfficeRequestExample : IExamplesProvider<UpdateOfficeRequest>
    {
        public UpdateOfficeRequest GetExamples() =>
            new()
            {
                City = "Brest",
                Street = "Lenina",
                HouseNumber = "30",
                OfficeNumber = "15",
                RegistryPhoneNumber = "8023296225",
                IsActive = false
            };
    }
}
