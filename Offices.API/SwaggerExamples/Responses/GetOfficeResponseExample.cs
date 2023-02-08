using Shared.Models.Response.Offices;
using Swashbuckle.AspNetCore.Filters;

namespace Offices.API.SwaggerExamples.Responses
{
    public class GetOfficeResponseExample : IExamplesProvider<OfficeResponse>
    {
        public OfficeResponse GetExamples()
        {
            return new OfficeResponse
            {
                PhotoId = Guid.NewGuid(),
                Address = "Homel Belickogo 9 1",
                RegistryPhoneNumber = "88005553535",
                IsActive = true
            };
        }
    }
}
