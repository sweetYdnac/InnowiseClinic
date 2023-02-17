namespace Shared.Models.Response.Offices
{
    public class GetOfficesResponse : PagedResponse
    {
        public IEnumerable<OfficeInformationResponse> Offices { get; init; }
        public GetOfficesResponse(
            IEnumerable<OfficeInformationResponse> offices,
            int currentPage,
            int pageSize,
            int totalCount)
            : base(currentPage, pageSize, totalCount) =>
            Offices = offices;
    }
}
