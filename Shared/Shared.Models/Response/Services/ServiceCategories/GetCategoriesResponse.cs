namespace Shared.Models.Response.Services.ServiceCategories
{
    public class GetCategoriesResponse
    {
        public IEnumerable<ServiceCategoryResponse> Categories { get; set; }
    }
}
