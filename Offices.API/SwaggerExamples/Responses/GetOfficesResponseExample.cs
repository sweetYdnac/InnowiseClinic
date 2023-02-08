using Shared.Models.Response.Offices;
using Swashbuckle.AspNetCore.Filters;

namespace Offices.API.SwaggerExamples.Responses
{
    public class GetOfficesResponseExample : IExamplesProvider<GetOfficesResponseModel>
    {
        public GetOfficesResponseModel GetExamples() =>
            new GetOfficesResponseModel(
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
