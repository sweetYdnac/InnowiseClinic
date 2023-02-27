using Services.Data.DTOs.Specialization;
using Shared.Models.Response;
using Shared.Models.Response.Services.Specialization;

namespace Services.Business.Interfaces
{
    public interface ISpecializationService
    {
        Task<SpecializationResponse> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreateSpecializationDTO dto);
        Task<PagedResponse<SpecializationResponse>> GetPagedAsync(GetSpecializationsDTO dto);
        Task ChangeStatusAsync(Guid id, bool isActive);
        Task UpdateAsync(Guid id, UpdateSpecializationDTO dto);
    }
}
