namespace Shared.Models.Response.Offices
{
    public class GetOfficesResponseModel : PagedResponse
    {
        public IEnumerable<OfficePreviewResponse> Offices { get; init; }
        public GetOfficesResponseModel(IEnumerable<OfficePreviewResponse> offices, int currentPage, int pageSize, int totalCount) 
            : base(currentPage, pageSize, totalCount) => Offices = offices;
    }
}
