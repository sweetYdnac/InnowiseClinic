namespace Shared.Models.Response.Profiles.Receptionist
{
    public class GetReceptionistsResponse : PagedResponse
    {
        public IEnumerable<ReceptionistInformationResponse> Receptionists { get; init; }

        public GetReceptionistsResponse(
            IEnumerable<ReceptionistInformationResponse> receptionists,
            int currentPage,
            int pageSize,
            int totalCount)
            : base(currentPage, pageSize, totalCount) =>
            Receptionists = receptionists;
    }
}
