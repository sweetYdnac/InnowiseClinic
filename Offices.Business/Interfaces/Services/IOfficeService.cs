using Offices.Data.DTOs;
using Shared.Models.Response;
using Shared.Models.Response.Offices;

namespace Offices.Business.Interfaces.Services
{
    public interface IOfficeService
    {
        Task<GetOfficesResponseModel> GetOfficesAsync(GetPagedOfficesDTO dto);
        Task<OfficeResponse> GetByIdAsync(Guid id);
        Task<Status201Response> CreateAsync(CreateOfficeDTO dto);
        Task ChangeStatus(ChangeOfficeStatusDTO dto);
        Task UpdateAsync(Guid id, UpdateOfficeDTO dto);
    }
}
