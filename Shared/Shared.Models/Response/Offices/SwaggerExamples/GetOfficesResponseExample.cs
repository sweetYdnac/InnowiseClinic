using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Offices.SwaggerExamples
{
    public class GetOfficesResponseExample : IExamplesProvider<PagedResponse<OfficeInformationResponse>>
    {
        public PagedResponse<OfficeInformationResponse> GetExamples() =>
            new(
                new[]
                {
                    new OfficeInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        Address = "Homel Belickogo 9 1",
                        RegistryPhoneNumber = "8023292724",
                        IsActive = true
                    },
                    new OfficeInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        Address = "Minsk Lenina 34 2",
                        RegistryPhoneNumber = "8023288888",
                        IsActive = false
                    }
                },
                2,
                10,
                20
                );
    }
}
