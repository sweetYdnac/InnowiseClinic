using Services.Data.DTOs;

namespace Services.Business.Interfaces
{
    public interface ISpecializationService
    {
        Task<Guid> CreateAsync(CreateSpecializationDTO dto);
    }
}
