namespace Shared.Models.Response.Offices
{
    public class GetOfficesResponseModel : PagedResponse
    {
        public IEnumerable<OfficeInformationResponse> Offices { get; init; }
        public GetOfficesResponseModel(IEnumerable<OfficeInformationResponse> offices, int currentPage, int pageSize, int totalCount) 
            : base(currentPage, pageSize, totalCount) => Offices = offices;
    }
}
