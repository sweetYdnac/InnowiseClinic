using Profiles.Data.DTOs.Patient;
using Shared.Models;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Data.Interfaces.Repositories
{
    public interface IPatientsRepository
    {
        Task<PatientResponse> GetByIdAsync(Guid id);
        Task<PagedResult<PatientInformationResponse>> GetPatients(GetPatientsDTO dto);
        Task AddAsync(CreatePatientDTO dto);
        Task<PatientResponse> GetMatchAsync(GetMatchedPatientDTO dto);
        Task<int> RemoveAsync(Guid id);
        Task<int> UpdateAsync(Guid id, UpdatePatientDTO dto);
        Task<Guid?> GetPhotoIdAsync(Guid id);
    }
}
