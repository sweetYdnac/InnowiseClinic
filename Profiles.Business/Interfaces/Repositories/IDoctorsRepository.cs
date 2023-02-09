using Profiles.Data.DTOs.Doctor;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IDoctorsRepository
    {
        Task<DoctorResponse> GetByIdAsync(Guid Id);
        Task<(IEnumerable<DoctorInformationResponse> doctors, int totalCount)> GetDoctors(GetDoctorsDTO dto);
        Task<int> AddAsync(CreateDoctorDTO dto);
    }
}
