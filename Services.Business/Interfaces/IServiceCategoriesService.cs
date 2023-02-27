using Shared.Models.Response.Services.ServiceCategories;

namespace Services.Business.Interfaces
{
    public interface IServiceCategoriesService
    {
        Task<ServiceCategoryResponse> GetByIdAsync(Guid id);
        Task<GetCategoriesResponse> GetAllAsync();
    }
}
