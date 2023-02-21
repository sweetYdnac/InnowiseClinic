using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Offices.SwaggerExamples
{
    public class GetOfficeResponseExample : IExamplesProvider<OfficeResponse>
    {
        public OfficeResponse GetExamples() =>
            new()
            {
                PhotoId = Guid.NewGuid(),
                Address = "Homel Belickogo 9 1",
                RegistryPhoneNumber = "88005553535",
                IsActive = true
            };
    }
}
