using Profiles.Application.Features.Doctor.Queries;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IDoctorsRepository
    {
        Task<DoctorInformationResponse> GetByIdAsync(Guid Id);
        Task<(IEnumerable<DoctorInformationResponse> doctors, int totalCount)> GetDoctors(GetDoctorsInformationQuery request);
    }
}
