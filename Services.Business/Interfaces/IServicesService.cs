using Services.Data.DTOs.Service;
using Shared.Models.Response;
using Shared.Models.Response.Services.Service;

namespace Services.Business.Interfaces
{
    public interface IServicesService
    {
        Task<ServiceResponse> GetByIdAsync(Guid id);
        Task<PagedResponse<ServiceInformationResponse>> GetPagedAsync(GetServicesDTO dto);
        Task<Guid> CreateAsync(CreateServiceDTO dto);
        Task ChangeStatusAsync(Guid id, bool isActive);
        Task UpdateAsync(Guid id, UpdateServiceDTO dto);
    }
}
