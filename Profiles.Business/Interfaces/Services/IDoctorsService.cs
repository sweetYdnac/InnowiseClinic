using Profiles.Data.DTOs.Doctor;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Business.Interfaces.Services
{
    public interface IDoctorsService
    {
        Task<DoctorInformationResponse> GetByIdAsync(Guid id);
        Task<GetDoctorsResponseModel> GetPagedAndFilteredAsync(GetDoctorsDTO dto);
    }
}
