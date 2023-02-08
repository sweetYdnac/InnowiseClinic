using Profiles.Data.DTOs.Patient;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IPatientsRepository
    {
        Task<PatientResponse> GetByIdAsync(Guid id);
        Task<(IEnumerable<PatientInformationResponse> patients, int totalCount)> GetPatients(GetPatientsDTO dto);
        Task<int> AddAsync(CreatePatientDTO dto);
        Task<PatientResponse> GetMatchAsync(GetMatchedPatientDTO dto);
        Task<int> RemoveAsync(Guid id);
        Task<int> UpdateAsync(Guid id, UpdatePatientDTO dto);
        Task<int> LinkToAccount(Guid id, Guid accountId);
    }
}
