using Profiles.Data.DTOs.Doctor;
using Shared.Core.Enums;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IDoctorsRepository
    {
        Task<DoctorResponse> GetByIdAsync(Guid Id);
        Task<(IEnumerable<DoctorInformationResponse> doctors, int totalCount)> GetDoctors(GetDoctorsDTO dto);
        Task<int> AddAsync(CreateDoctorDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateDoctorDTO dto);
        Task<int> RemoveAsync(Guid id);
    }
}
