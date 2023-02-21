using Offices.Data.DTOs;
using Shared.Models;
using Shared.Models.Response.Offices;

namespace Offices.Data.Interfaces.Repositories
{
    public interface IOfficeRepository
    {
        Task<OfficeResponse> GetByIdAsync(Guid id);
        Task<PagedResult<OfficeInformationResponse>> GetPagedOfficesAsync(GetPagedOfficesDTO dto);
        Task AddAsync(CreateOfficeDTO dto);
        Task<int> ChangeStatusAsync(ChangeOfficeStatusDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateOfficeDTO dto);
    }
}
