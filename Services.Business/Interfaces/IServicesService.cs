using Services.Data.DTOs.Service;
using Shared.Models.Response;
using Shared.Models.Response.Services.Service;

namespace Services.Business.Interfaces
{
    public interface IServicesService
    {
        Task<ServiceResponse> GetByIdAsync(Guid id);
        Task<PagedResponse<ServiceResponse>> GetPagedAsync(GetServicesDTO dto);
        Task<Guid> CreateAsync(CreateServiceDTO dto);
    }
}
