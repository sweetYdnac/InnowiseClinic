using Profiles.Data.DTOs.Doctor;
using Shared.Core.Enums;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Business.Interfaces.Services
{
    public interface IDoctorsService
    {
        Task<DoctorResponse> GetByIdAsync(Guid id);
        Task<GetDoctorsResponseModel> GetPagedAndFilteredAsync(GetDoctorsDTO dto);
        Task<Guid?> CreateAsync(CreateDoctorDTO dto);
        Task UpdateAsync(Guid id, UpdateDoctorDTO dto);
        Task RemoveAsync(Guid id);
        Task ChangeStatus(Guid id, AccountStatuses status);
    }
}
