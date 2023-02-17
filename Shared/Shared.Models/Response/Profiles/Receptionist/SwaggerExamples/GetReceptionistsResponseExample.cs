using Shared.Core.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Response.Profiles.Receptionist.SwaggerExamples
{
    public class GetReceptionistsResponseExample : IExamplesProvider<GetReceptionistsResponse>
    {
        public GetReceptionistsResponse GetExamples() =>
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
