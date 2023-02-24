using Services.Data.DTOs;
using Shared.Models.Response.Services.Specialization;

namespace Services.Business.Interfaces
{
    public interface ISpecializationService
    {
        Task<SpecializationResponse> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreateSpecializationDTO dto);
    }
}
