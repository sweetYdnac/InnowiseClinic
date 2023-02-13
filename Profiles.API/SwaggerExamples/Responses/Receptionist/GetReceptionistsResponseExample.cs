using Shared.Core.Enums;
using Shared.Models.Response.Profiles.Receptionist;
using Swashbuckle.AspNetCore.Filters;

namespace Profiles.API.SwaggerExamples.Responses.Receptionist
{
    public class GetReceptionistsResponseExample : IExamplesProvider<GetReceptionistsResponseModel>
    {
        public GetReceptionistsResponseModel GetExamples() =>
            new(
                new[]
                {
                    new ReceptionistInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Jonny Cage someMiddleName",
                        OfficeAddress = "Boston somestreet 10 9",
                        Status = AccountStatuses.AtWork,
                    },
                    new ReceptionistInformationResponse
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Will Smith someMiddleName",
                        OfficeAddress = "Toronto somestreet 22 2",
                        Status = AccountStatuses.AtWork,
                    }
                },
                5,
                2,
                50
                );
    }
}
