using Offices.Data.DTOs;
using Shared.Models.Response;
using Shared.Models.Response.Offices;

namespace Offices.Business.Interfaces.Repositories
{
    public interface IOfficeRepository
    {
        Task<OfficeResponse> GetByIdAsync(Guid id);
        Task<Status201Response> CreateAsync(CreateOfficeDTO dto);
        Task<int> ChangeStatusAsync(ChangeOfficeStatusDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateOfficeDTO dto);
    }
}
