using Shared.Models.Response.Profiles.Receptionist;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Receptionist
{
    public class GetReceptionistsResponseExample : IExamplesProvider<GetReceptionistsResponseModel>
    {
        public GetReceptionistsResponseModel GetExamples() =>
            new GetReceptionistsResponseModel(
                new[]
                {
                    new ReceptionistInformationResponse
                    {
                        FullName = "Jonny Cage someMiddleName",
                        OfficeAddress = "Boston somestreet 10 9"
                    },
                    new ReceptionistInformationResponse
                    {
                        FullName = "Will Smith someMiddleName",
                        OfficeAddress = "Toronto somestreet 22 2"
                    }
                },
                5,
                2,
                50
                );
    }
}
